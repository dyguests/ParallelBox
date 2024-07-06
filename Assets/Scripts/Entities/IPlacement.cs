﻿using System;
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
            target.Pos = source.Pos;
        }

        public abstract IPlacement DeepClone();

        #endregion
    }
}