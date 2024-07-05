namespace Entities
{
    public interface IGround : IPlacement { }

    public class Ground : Placement, IGround { }
}