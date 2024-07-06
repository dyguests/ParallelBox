using UnityEngine;

namespace Entities
{
    public interface IPlayer : IMovement { }

    public class Player : Placement, IPlayer
    {
        #region IMovement

        public void Moved(Vector2Int start, Vector2Int end)
        {
            MovementDelegate.Moved(this, start, end);
            // todo notify
        }

        #endregion
    }
}