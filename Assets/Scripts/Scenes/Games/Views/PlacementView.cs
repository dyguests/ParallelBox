using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Frameworks;
using Scenes.Games.Views;

namespace Scenes.Games
{
    public interface IPlacementView
    {
        UniTask Delete();
    }

    public class PlacementView<TData> : DataView<TData>, IPlacementView
        where TData : IPlacement
    {
        #region DataView<TData>

        public override async UniTask LoadData(TData data)
        {
            await base.LoadData(data);
            // todo 之后移到 ApplyChange 中去
            transform.localPosition = PlateView.Pos2Local(Data);
        }

        public override async UniTask UnloadData()
        {
            await base.UnloadData();
        }

        #endregion

        #region IPlacementView

        public async UniTask Delete()
        {
            await UnloadData();
            PlateView = null;
            Destroy(gameObject);
        }

        #endregion

        #region PlacementView<TData>

        protected PlateView PlateView { get; set; }

        #endregion
    }
}