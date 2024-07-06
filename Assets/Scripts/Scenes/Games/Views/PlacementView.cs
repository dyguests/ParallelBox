using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Frameworks;
using Koyou.Recordables;
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

        private IDisperser _disperser;

        public override async UniTask LoadData(TData data)
        {
            await base.LoadData(data);
            _disperser = Data.Collect<TData>(ApplyChange);
        }

        public override async UniTask UnloadData()
        {
            _disperser.Disperse();
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

        private void ApplyChange(TData previous, TData current, List<ITransition> transitions)
        {
            // todo 动画
            transform.localPosition = PlateView.Pos2Local(current);
        }

        #endregion
    }
}