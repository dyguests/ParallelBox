namespace Entities
{
    public interface IWall : IPlacement { }

    public class Wall : Placement, IWall { }
}