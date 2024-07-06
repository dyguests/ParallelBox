using Entities;
using Koyou.Frameworks;

namespace Scenes.Games
{
    public interface IPlacementView { }

    public class PlacementView<TData> : DataView<TData>, IPlacementView
        where TData : IPlacement
    {
        #region PlacementView<TData>

        protected PlateView PlateView { get; set; }

        #endregion
    }
}