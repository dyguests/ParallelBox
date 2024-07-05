using Entities;
using Koyou.Frameworks;

namespace Scenes.Games
{
    public class GroundView : PlacementView<IGround> { }

    public class PlacementView<TData> : DataView<TData>
        where TData : IPlacement { }
}