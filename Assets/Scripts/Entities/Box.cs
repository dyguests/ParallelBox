using UnityEngine;

namespace Entities
{
    public class Box : Placement, IMovement
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