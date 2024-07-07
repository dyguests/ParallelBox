using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Commons;
using Koyou.Frameworks;
using Koyou.Recordables;
using UnityEngine;

namespace Scenes.Games.Views
{
    public class GameView : DataView<IGame>
    {
        #region DataView<IGame>

        private IDisperser _disperser;

        public override async UniTask LoadData(IGame data)
        {
            await base.LoadData(data);
            _disperser = Data.Collect<IGame>(async (game, current, list) => await ApplyChange(game, current, list));

            PlateViewport.Id = 0;
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

            PlateViewport.Id = 0;

            _disperser.Disperse();
            await base.UnloadData();
        }

        #endregion

        #region GameView

        [SerializeField] private GameInput gameInput;

        private readonly Dictionary<IPlate, PlateViewport> _plateViewports = new();

        private async UniTask ApplyChange(IGame previous, IGame current, List<ITransition> transitions)
        {
            var hasViewportChanged = false;
            foreach (var transition in transitions ?? new List<ITransition>())
            {
                switch (transition)
                {
                    case Game.AddPlatesTransition addPlatesTransition:
                    {
                        foreach (var plate in addPlatesTransition.Plates)
                        {
                            await InsertPlateViewport(plate);
                            hasViewportChanged = true;
                        }

                        break;
                    }
                    default: break;
                }
            }

            if (hasViewportChanged)
            {
                await RearrangePlateViewports();
            }
        }

        private async UniTask RearrangePlateViewports()
        {
            var plates = Data.Plates.ToArray();
            var count = plates.Length;
            for (var i = 0; i < count; i++)
            {
                var plate = plates[i];
                var plateViewport = _plateViewports[plate];
                await plateViewport.RearrangeViewport(i, count);
            }
        }


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