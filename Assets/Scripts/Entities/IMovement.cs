using UnityEngine;

namespace Entities
{
    public interface IMovement : IPlacement
    {
        void Moved(Vector2Int start, Vector2Int end);
    }

    public static class MovementDelegate
    {
        public static void Moved(IMovement movement, Vector2Int start, Vector2Int end)
        {
            movement.Pos = end;
        }
    }
}