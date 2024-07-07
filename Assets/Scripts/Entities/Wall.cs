namespace Entities
{
    public interface IWall : IPlacement { }

    public class Wall : Placement, IWall
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Wall>(
            PlacementSavior,
            source => new Wall(),
            (source, target) =>
            {
                /*target.Specie = source.Specie;*/
            },
            source => null
        );

        #endregion

        #region IDeepCloneable<IPlacement>

        public override IPlacement SplitClone(int count)
        {
            var clone = new Wall();
            PlacementSplitClone(this, clone, count);
            return clone;
        }

        #endregion

        #region Placement

        public override int Layer => 2;

        #endregion

        #region Wall

        public Wall(Ratio ratio = default) : base(ratio) { }

        #endregion
    }
}