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

        #region Placement

        public override int Layer => 2;

        #endregion
    }
}