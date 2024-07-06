using UnityEngine;

namespace Entities
{
    public interface IPlayer : IControllable { }

    public class Player : Placement, IPlayer
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Player>(
            PlacementSavior,
            source => new Player(),
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

        public override IPlacement DeepClone()
        {
            var clone = new Player();
            PlacementDeepClone(this, clone);
            return clone;
        }

        #endregion

        #region Placement

        public override int Layer => 2;

        #endregion
    }
}