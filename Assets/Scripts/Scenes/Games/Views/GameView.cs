using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Commons;
using Koyou.Frameworks;
using Koyou.Recordables;
using Repositories;
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

            await uiView.LoadData(Data);

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

            await uiView.UnloadData();

            _disperser.Disperse();
            await base.UnloadData();
        }

        #endregion

        #region GameView

        [SerializeField] private GameInput gameInput;

        [Space] [SerializeField] private UiView uiView;

        private readonly Dictionary<IPlate, PlateViewport> _plateViewports = new();

        private async UniTask ApplyChange(IGame previous, IGame current, List<ITransition> transitions)
        {
            if (transitions != null)
            {
                var hasCompleted = transitions.Any(transition => transition is Game.CompletedTransition);
                if (hasCompleted)
                {
                    var nextLevelIndex = (GamePrefs.CurrentLevelIndex + 1) % GameDatas.Count;
                    GamePrefs.CurrentLevelIndex = nextLevelIndex;
                    var index = nextLevelIndex;
                    var game = GameDatas.GetLevel(index);
                    await UniTask.Delay(2000);
                    AppStateMachine.Instance.EnqueueState(new GameAppState(game));
                    return;
                }
            }

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

            public void Restart()
            {
                var index = GamePrefs.CurrentLevelIndex;
                var game = GameDatas.GetLevel(index);
                AppStateMachine.Instance.EnqueueState(new GameAppState(game));
            }

            public void Undo()
            {
                return;
                
                var data = _owner.Data;
                if (data.Undoable())
                {
                    data.Undo();
                }
            }

            public void Redo()
            {
                return;

                var data = _owner.Data;
                if (data.Redoable())
                {
                    data.Redo();
                }
            }

            #endregion

            #region InputCallback

            private readonly GameView _owner;

            public InputCallback(GameView owner)
            {
                _owner = owner;
            }

            #endregion
        }

        #endregion
    }
}