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

        #region IDeepCloneable<IPlacement>

        public override IPlacement SplitClone(int count)
        {
            var clone = new Goal();
            PlacementSplitClone(this, clone, count);
            return clone;
        }

        #endregion

        #region Placement

        public override int Layer => 1;

        #endregion

        #region Goal

        public Goal(Ratio ratio = default) : base(ratio) { }

        #endregion
    }
}