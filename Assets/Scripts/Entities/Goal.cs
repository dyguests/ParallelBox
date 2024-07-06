namespace Entities
{
    public interface IGoal : IPlacement { }

    public class Goal : Placement, IGoal
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Goal>(
            PlacementSavior,
            source => new Goal(),
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