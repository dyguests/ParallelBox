using UnityEngine;

namespace Entities
{
    public interface IBox : IMovement { }

    public class Box : Placement, IBox
    {
        #region Placement

        public override int Layer => 2;

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