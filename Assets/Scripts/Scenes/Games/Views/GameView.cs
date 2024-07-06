using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Frameworks;
using Scenes.Games.Views;
using UnityEngine;

namespace Scenes.Games
{
    public class GameView : DataView<IGame>
    {
        #region DataView<IGame>

        public override async UniTask LoadData(IGame data)
        {
            await base.LoadData(data);
            await plateView.LoadData(Data.Plate);
        }

        public override async UniTask UnloadData()
        {
            await plateView.UnloadData();
            await base.UnloadData();
        }

        #endregion

        #region GameView

        [SerializeField] private PlateView plateView;

        #endregion
    }
}