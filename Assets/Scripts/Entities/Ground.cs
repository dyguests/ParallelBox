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

        #region Placement

        public override int Layer => 0;

        #endregion
    }
}