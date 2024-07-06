using UnityEngine;

namespace Entities
{
    public interface IPlayer : IControllable { }

    public class Player : Placement, IPlayer
    {
        #region Placement

        public override int Layer => 5;

        #endregion

        #region IMovement

        public void Moved(Vector2Int start, Vector2Int end)
        {
            MovementDelegate.Moved(this, start, end);
            // todo notify
        }

        #endregion
    }
}