using System;
using JetBrains.Annotations;
using Koyou.Recordables;
using UnityEngine;

namespace Entities
{
    public interface IPlacement : IRecordable, ISplitCloneable<IPlacement>
    {
        [RecordlessField] public IPlate Plate { get; }

        Vector2Int Pos { get; set; }

        /**
         * this在世界中的占比
         * 占比 = Ratio.x/Ratio.y
         * Ratio.z = 0 表示不可变，1 表示跟随世界分裂而修改占比
         */
        Ratio Ratio { get; set; }

        /// <summary>
        /// 在同一个Pos能放多个IPlacement，且一种Level仅能放置一个
        /// </summary>
        int Layer { get; }

        void Inserted([NotNull] IPlate plate, Vector2Int pos);
        void Removed();

        void Splitted(int count);
    }

    public abstract class Placement : RecordableObject, IPlacement
    {
        #region RecordableObject

        protected static readonly Saver<Placement> PlacementSavior = new(
            source => throw new NotImplementedException(),
            (source, target) =>
            {
                target.Pos = source.Pos;
                target.Ratio = source.Ratio;
            },
            null
        );

        #endregion

        #region IPlacement

        public IPlate Plate { get; private set; }

        public Vector2Int Pos { get; set; }
        public Ratio Ratio { get; set; } = new Ratio(1, 1, true);
        public virtual int Layer => 0;

        public void Inserted(IPlate plate, Vector2Int pos)
        {
            Plate = plate;
            Pos = pos;
            //todo notify
        }

        public void Removed()
        {
            Plate = null;
            // todo notify
        }

        public void Splitted(int count)
        {
            Ratio = Ratio.Split(count);
        }

        #endregion

        #region IDeepCloneable<IPlacement>

        protected static void PlacementSplitClone(IPlacement source, IPlacement target, int count)
        {
            ((Placement)target).Plate = source.Plate;
            target.Pos = source.Pos;
            target.Ratio = source.Ratio.Split(count);
        }

        public abstract IPlacement SplitClone(int count);

        #endregion
    }
}