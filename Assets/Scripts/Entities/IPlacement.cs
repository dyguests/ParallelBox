using System;
using JetBrains.Annotations;
using Koyou.Commons;
using Koyou.Recordables;
using UnityEngine;

namespace Entities
{
    public interface IPlacement : IRecordable, IDeepCloneable<IPlacement>
    {
        [RecordlessField] public IPlate Plate { get; }

        Vector2Int Pos { get; set; }

        /**
         * this在世界中的占比
         * 占比 = Ratio.x/Ratio.y
         * Ratio.z = 0 表示不可变，1 表示跟随世界分裂而修改占比
         */
        Vector3Int Ratio { get; set; }

        /// <summary>
        /// 在同一个Pos能放多个IPlacement，且一种Level仅能放置一个
        /// </summary>
        int Layer { get; }

        void Inserted([NotNull] IPlate plate, Vector2Int pos);
        void Removed();
    }

    public abstract class Placement : RecordableObject, IPlacement
    {
        #region RecordableObject

        protected static readonly Saver<Placement> PlacementSavior = new(
            source => throw new NotImplementedException(),
            (source, target) => { target.Pos = source.Pos; },
            null
        );

        #endregion

        #region IPlacement

        public IPlate Plate { get; private set; }

        public Vector2Int Pos { get; set; }
        public Vector3Int Ratio { get; set; } = new Vector3Int(1, 1, 0);
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

        #endregion

        #region IDeepCloneable<IPlacement>

        protected static void PlacementDeepClone(IPlacement source, IPlacement target)
        {
            ((Placement)target).Plate = source.Plate;
            target.Ratio = source.Ratio; // todo 分裂
            target.Pos = source.Pos;
        }

        public abstract IPlacement DeepClone();

        #endregion
    }
}