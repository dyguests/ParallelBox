namespace Entities
{
    public interface IWall : IPlacement { }

    public class Wall : Placement, IWall
    {
        #region Placement

        public override int Layer => 3;

        #endregion
    }
}