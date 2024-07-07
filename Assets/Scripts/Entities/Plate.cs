using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Koyou.Commons;
using Koyou.Recordables;
using UnityEngine;

namespace Entities
{
    public interface IPlate : IRecordable, ISplitCloneable<IPlate>
    {
        Vector2Int Size { get; }

        [RecordlessField] IControllable Controllable { get; }

        bool IsCompleted { get; }

        /// <summary>
        /// 这里是控制 Controllable Move,进而推箱子
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="splittingMovements"></param>
        /// <returns></returns>
        bool Move(Vector2Int direction, out List<IMovement> splittingMovements);

        /// <summary>
        /// 这里是Plate分裂的逻辑
        /// 
        /// 若没有位置能够分裂，则返回空
        /// 若仅能分裂一个，自身改成分裂后的状态，返回空。
        /// 若能分裂多个，自身改成分裂后的一个状态，并返回其它分裂后的状态。
        /// </summary>
        /// <param name="movement"></param>
        List<IPlate> Split(IMovement movement);

        bool Contains(Vector2Int pos);
        void Insert(Vector2Int pos, IPlacement placement);
        void Remove(Vector2Int pos, int layer);

        IEnumerable<IPlacement> Get(Vector2Int pos);
        T Get<T>(Vector2Int pos) where T : IPlacement;

        void Move(Vector2Int start, Vector2Int end, IMovement movement);
    }

    public class Plate : RecordableObject, IPlate
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Plate>(
            null,
            source => new Plate(source.Size),
            (source, target) => { source.Size.ForEach(pos => { target.Cells[pos.x, pos.y] = source.Cells[pos.x, pos.y]?.Clone(); }); },
            source => source.Cells.Cast<ICell>());

        #endregion

        #region IPlate

        public Vector2Int Size { get; }

        public IControllable Controllable => _controllable ??= Size.GetEnumerator().SelectMany(Get).OfType<IControllable>().FirstOrDefault().RequireNotNull();

        public bool IsCompleted => Size.GetEnumerator()
            .Select(Get)
            .Where(Predicates.NotNull)
            .All(placements =>
            {
                var array = placements.ToArray();
                var goal = array.OfType<IGoal>().FirstOrDefault();
                if (goal == null) return true;
                var box = array.OfType<IBox>().FirstOrDefault();
                return box != null;
            });

        public bool Move(Vector2Int direction, [NotNull] out List<IMovement> splittingMovements)
        {
            var movements = new List<IMovement> { Controllable };

            // move
            while (true)
            {
                var nextPoses = movements.Select(movement => movement.Pos)
                    .Select(pos => pos + direction)
                    .ToList();
                if (nextPoses.Any(pos => !Contains(pos)))
                {
                    splittingMovements = new List<IMovement>();
                    return false;
                }

                // movement.ratio <= ground.ratio, pass
                // movement.ratio >= wall.ratio, pass
                if (!movements.All(movement =>
                    {
                        var nextGround = Get<IGround>(movement.Pos + direction);
                        if (nextGround == null) return false;
                        if (movement.Ratio.Value > nextGround.Ratio.Value) return false;

                        var nextWall = Get<IWall>(movement.Pos + direction);
                        if (nextWall == null) return true;
                        if (movement.Ratio.Value <= nextWall.Ratio.Value) return false;

                        return true;
                    }))
                {
                    splittingMovements = new List<IMovement>();
                    return false;
                }

                var collides = nextPoses
                    .Select(Get)
                    .Where(Predicates.NotNull)
                    .SelectMany(placements => placements)
                    .Where(placement => placement.Layer == Controllable.Layer) // 仅检查同层碰撞
                    .Where(placement => !movements.Contains(placement))
                    .ToList();
                if (!collides.Any())
                {
                    foreach (var movement in movements)
                    {
                        Move(movement.Pos, movement.Pos + direction, movement);
                    }

                    break;
                }

                // todo 禁用连续推多个 movement; 之后确认要不要加此功能
                // 这里改成连同自己，一共只有两个movement可以移动
                if (collides.Count > 2)
                {
                    splittingMovements = new List<IMovement>();
                    return false;
                }

                if (collides.Any(placement => placement is not IMovement))
                {
                    splittingMovements = new List<IMovement>();
                    return false;
                }

                movements.AddRange(collides.Cast<IMovement>());
            }

            // split
            splittingMovements = movements.Where(movement => Get<ISplitter>(movement.Pos) != null).ToList();

            return true;
        }

        public List<IPlate> Split(IMovement movement)
        {
            var startPos = movement.Pos;
            var splitter = Get<ISplitter>(startPos);
            if (splitter == null) return new List<IPlate>();

            var splittableDirections = splitter.GetSplitDirections()
                // todo test
                .Select(direction =>
                {
                    Log.N($"splittableDirections: {direction}");
                    return direction;
                })
                .Where(direction => Get(movement.Pos + direction).None(placement => placement.Layer == movement.Layer))
                .ToList();

            var splitCount = splittableDirections.Count;

            // 分裂出的Plates
            var splitPlates = new List<IPlate>();
            for (var i = 0; i < splitCount - 1; i++)
            {
                splitPlates.Add(this.SplitClone(splitCount));
            }

            var enumerator = splittableDirections.GetEnumerator();
            using (enumerator)
            {
                if (enumerator.MoveNext())
                {
                    var direction = enumerator.Current;
                    Move(startPos, startPos + direction, movement);
                    Splitted(splitCount);
                }

                foreach (var splitPlate in splitPlates)
                {
                    Debug.Assert(enumerator.MoveNext());

                    var splitMovement = splitPlate.Get<IMovement>(startPos);
                    Debug.Assert(splitMovement != null);

                    var direction = enumerator.Current;
                    splitPlate.Move(startPos, startPos + direction, splitMovement);
                }
            }

            return splitPlates;
        }

        /// <summary>
        /// TODO FIXME
        /// </summary>
        /// <param name="count"></param>
        private void Splitted(int count)
        {
            Size.GetEnumerator()
                .Select(pos => Cells.Get(pos))
                .Where(Predicates.NotNull)
                .ForEach(cell => cell.Splitted(count));
        }

        public bool Contains(Vector2Int pos) => Size.Contains(pos);

        public void Insert(Vector2Int pos, IPlacement placement)
        {
            var cell = Cells.GetOrPut(pos, () => new Cell());
            var lastItem = cell.Get(placement.Layer);
            if (lastItem != null) Remove(pos, placement.Layer);
            cell.Set(placement);
            placement.Inserted(this, pos);
            // AddTransition(InsertTransition.From(pos, placement));
        }

        public void Remove(Vector2Int pos, int layer)
        {
            var cell = Cells.Get(pos);
            if (cell == null) return;
            var placement = cell.Get(layer);
            if (placement == null) return;
            cell.Remove(placement);
            placement.Removed();
            // AddTransition(RemoveTransition.From(pos, placement));
        }

        public IEnumerable<IPlacement> Get(Vector2Int pos) => Cells.Get(pos) ?? Enumerable.Empty<IPlacement>();
        public T Get<T>(Vector2Int pos) where T : IPlacement => Cells.Get(pos).OfType<T>().FirstOrDefault();

        public void Move(Vector2Int start, Vector2Int end, IMovement movement)
        {
            if (!Contains(end)) throw new IndexOutOfRangeException($"Pos({end.x},{end.y}) is out of range.");
            if (start == end) throw new InvalidOperationException($"start({start})==end({end})");

            // 由于同时处理多个元素移动时，可能出现覆盖的情况。所以当前判断未被覆盖时，才走移除操作。
            if (Cells.Get(start)?.Any(placement => placement == movement) == true)
            {
                Cells.Get(start).Remove(movement);
            }

            Cells.GetOrPut(end, () => new Cell()).Set(movement);

            movement.Moved(start, end);

            //  AddTransition(MoveTransition.From(start, end, movement));
        }

        #endregion

        #region IDeepCloneable<IPlate>

        public IPlate SplitClone(int count)
        {
            var clone = new Plate(Size);
            this.Size.ForEach(pos => { clone.Cells[pos.x, pos.y] = this.Cells[pos.x, pos.y]?.SplitClone(count); });
            return clone;
        }

        #endregion

        #region Plate

        private ICell[,] Cells { get; }

        private IControllable _controllable;

        public Plate(int width, int height) : this(new Vector2Int(width, height)) { }

        private Plate(Vector2Int size)
        {
            Size = size;
            Cells = new ICell[size.x, size.y];
        }

        #endregion
    }
}