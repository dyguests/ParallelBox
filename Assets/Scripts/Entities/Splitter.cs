using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public interface ISplitter : IPlacement
    {
        bool Left { get; }
        bool Up { get; }
        bool Right { get; }
        bool Down { get; }

        IEnumerable<Vector2Int> GetSplitDirections();
    }

    public class Splitter : Placement, ISplitter
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Splitter>(
            PlacementSavior,
            source => new Splitter(
                source.Left,
                source.Up,
                source.Right,
                source.Down
            ),
            (source, target) =>
            {
                /*target.Specie = source.Specie;*/
            },
            source => null
        );

        #endregion

        #region Placement

        public override int Layer => 1;

        #endregion

        #region ISplitter

        public bool Left { get; }
        public bool Up { get; }
        public bool Right { get; }
        public bool Down { get; }

        public IEnumerable<Vector2Int> GetSplitDirections()
        {
            if (Left) yield return Vector2Int.left;
            if (Up) yield return Vector2Int.up;
            if (Right) yield return Vector2Int.right;
            if (Down) yield return Vector2Int.down;
        }

        #endregion

        #region Splitter

        // todo 可以用 0~8来包含所有的分裂方向；及Vector2Int的八个方向，加上zero共九个方向，例如zero为4方向均分裂，up为上左右均分裂，one为上右分裂。

        public Splitter(bool left, bool up, bool right, bool down)
        {
            Left = left;
            Up = up;
            Right = right;
            Down = down;

            if (new[] { left, up, right, down }.Count(b => b) < 2)
            {
                // 分裂数量不能小于2
                throw new ArgumentException("At least two directions must be true.");
            }
        }

        #endregion
    }
}