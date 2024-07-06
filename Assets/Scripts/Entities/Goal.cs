namespace Entities
{
    public interface IGoal : IPlacement { }

    public class Goal : Placement, IGoal { }
}