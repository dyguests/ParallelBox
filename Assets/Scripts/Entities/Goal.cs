namespace Entities
{
    public interface IGoal : IPlacement { }

    public class Goal : Placement, IGoal
    {
        #region Placement

        public override int Layer => 2;

        #endregion
    }
}