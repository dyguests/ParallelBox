using System;
using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Commons;
using Koyou.Frameworks;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Scenes.Games
{
    public class GameAppState : AppState
    {
        #region AppState

        public override async UniTask Enter()
        {
            await base.Enter();
            await SceneManager.LoadSceneAsync("Game").ToUniTask();
            Log.N($"Called");
            var scene = Object.FindFirstObjectByType<GameScene>() ?? throw new Exception($"{nameof(GameScene)} not found");
            scene.Game = _game;
            await scene.Enter();
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }

        #endregion

        #region GameAppState

        private readonly IGame _game;

        public GameAppState(IGame game)
        {
            _game = game;
        }

        #endregion
    }
}