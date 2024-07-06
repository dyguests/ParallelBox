namespace Entities
{
    public interface IGround : IPlacement { }

    public class Ground : Placement, IGround
    {
        #region Placement

        public override int Layer => 1;

        #endregion
    }
}