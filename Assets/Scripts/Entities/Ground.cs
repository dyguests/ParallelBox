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

        public override IPlacement DeepClone()
        {
            var clone = new Ground();
            PlacementDeepClone(this, clone);
            return clone;
        }

        #endregion

        #region Placement

        public override int Layer => 0;

        #endregion
    }
}