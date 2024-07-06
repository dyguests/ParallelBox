namespace Entities
{
    public interface ISplitter : IPlacement
    {
        bool Left { get; }
        bool Up { get; }
        bool Right { get; }
        bool Down { get; }
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

        #endregion

        #region Splitter

        public Splitter(bool left, bool up, bool right, bool down)
        {
            Left = left;
            Up = up;
            Right = right;
            Down = down;
        }

        #endregion
    }
}