using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Commons;
using Koyou.Frameworks;
using UnityEngine;

namespace Scenes.Games.Views
{
    public class GameView : DataView<IGame>
    {
        #region DataView<IGame>

        public override async UniTask LoadData(IGame data)
        {
            await base.LoadData(data);
            // todo ApplyChange isCompleted

            foreach (var plate in Data.Plates)
            {
                await InsertPlateViewport(plate);
            }

            gameInput.callback = new InputCallback(this);
            gameInput.ActiveInput();
        }

        public override async UniTask UnloadData()
        {
            gameInput.InactiveInput();
            gameInput.callback = null;

            foreach (var plate in Data.Plates)
            {
                await RemovePlateViewport(plate);
            }

            await base.UnloadData();
        }

        #endregion

        #region GameView

        [SerializeField] private GameInput gameInput;

        private readonly Dictionary<IPlate, PlateViewport> _plateViewports = new();

        private async UniTask InsertPlateViewport(IPlate plate)
        {
            var plateViewport = await PlateViewport.Generate(plate, this);
            _plateViewports.Add(plate, plateViewport);
        }

        private async UniTask RemovePlateViewport(IPlate plate)
        {
            var plateViewport = _plateViewports[plate];
            _plateViewports.Remove(plate);
            await plateViewport.Delete();
        }

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