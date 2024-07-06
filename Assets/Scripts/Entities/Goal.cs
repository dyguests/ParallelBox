namespace Entities
{
    public interface IGoal : IPlacement { }

    public class Goal : Placement, IGoal
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Box>(
            PlacementSavior,
            source => new Box(),
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
    }
}