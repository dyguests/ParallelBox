using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Commons;
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
            gameInput.callback = new InputCallback(this);
        }

        public override async UniTask UnloadData()
        {
            await plateView.UnloadData();
            await base.UnloadData();
        }

        #endregion

        #region GameView

        [SerializeField] private GameInput gameInput;

        [Space] [SerializeField] private PlateView plateView;

        private class InputCallback : GameInput.ICallback
        {
            #region GameInput.ICallback

            public void Return()
            {
                // todo
            }

            public void Move(Vector2Int direction)
            {
                var moved = _owner.Data.Move(direction);
                Log.N($"direction: {direction}, moved: {moved}");
            }

            #endregion

            #region InputCallback

            private GameView _owner;

            public InputCallback(GameView owner)
            {
                _owner = owner;
            }

            #endregion
        }

        #endregion
    }
}