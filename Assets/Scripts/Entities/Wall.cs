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

        #region IDeepCloneable<IPlacement>

        public override IPlacement DeepClone()
        {
            var clone = new Wall();
            PlacementDeepClone(this, clone);
            return clone;
        }

        #endregion

        #region Placement

        public override int Layer => 2;

        #endregion
    }
}