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

        public override IPlacement SplitClone(int count)
        {
            var clone = new Player();
            PlacementSplitClone(this, clone, count);
            return clone;
        }

        #endregion

        #region Placement

        public override int Layer => 2;

        public Player(Ratio ratio = default)
        {
            // if default
            if (ratio.molecule == 0 || ratio.letter == 0)
            {
                ratio = new Ratio(1, 1, true);
            }

            Ratio = ratio;
        }

        #endregion
    }
}