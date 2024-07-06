﻿using System;
using System.Collections.Generic;
using System.Linq;
using Koyou.Commons;
using Koyou.Recordables;
using UnityEngine;

namespace Entities
{
    public interface IPlate : IRecordable
    {
        Vector2Int Size { get; }

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

        #region Plate

        private ICell[,] Cells { get; }

        public Plate(int width, int height) : this(new Vector2Int(width, height)) { }

        public Plate(Vector2Int size)
        {
            Size = size;
            Cells = new ICell[size.x, size.y];
        }

        #endregion
    }
}