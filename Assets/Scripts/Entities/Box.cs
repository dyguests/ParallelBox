using UnityEngine;

namespace Entities
{
    public interface IBox : IMovement { }

    public class Box : Placement, IBox
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Box>(
            PlacementSavior,
            source => new Box(),
            (source, target) =>
            {
                /*target.Specie = source.Specie;*/
            },
            source => null
        );

        #endregion

        #region IMovement

        public void Moved(Vector2Int start, Vector2Int end)
        {
            MovementDelegate.Moved(this, start, end);
            // todo notify
        }

        #endregion

        #region IDeepCloneable<IPlacement>

        public override IPlacement SplitClone(int count)
        {
            var clone = new Box();
            PlacementSplitClone(this, clone, count);
            return clone;
        }

        #endregion

        #region Placement

        public override int Layer => 2;

        #endregion

        #region Box

        public Box(Ratio ratio = default) : base(ratio) { }

        #endregion
    }
}