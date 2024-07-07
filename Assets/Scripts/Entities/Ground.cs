namespace Entities
{
    public interface IGround : IPlacement { }

    public class Ground : Placement, IGround
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Ground>(
            PlacementSavior,
            source => new Ground(),
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
            var clone = new Ground();
            PlacementSplitClone(this, clone, count);
            return clone;
        }

        #endregion

        #region Placement

        public override int Layer => 0;

        #endregion
    }
}