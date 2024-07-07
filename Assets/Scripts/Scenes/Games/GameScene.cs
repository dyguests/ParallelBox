using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Commons;
using Koyou.Frameworks;
using Repositories;
using Scenes.Games.Views;
using UnityEngine;

namespace Scenes.Games
{
    public class GameScene : BaseScene
    {
        #region MonoBehaviour

        private void Start()
        {
#if UNITY_EDITOR
            if (Game != null)
            {
                return;
            }

            var index = GamePrefs.CurrentLevelIndex;

            index = 2; // todo test

            Game = GameDatas.GetLevel(index);
            RunSceneFlow();
#endif
        }

        #endregion

        #region BaseScene

        public override async UniTask Enter()
        {
            await base.Enter();
            Log.N($"GameScene Enter Game: {Game}");
            await gameView.LoadData(Game);
        }

        public override async UniTask Exit()
        {
            await gameView.UnloadData();
            await base.Exit();
        }

        #endregion

        #region GameScene

        [SerializeField] private GameView gameView;

        public IGame Game { get; set; }

        #endregion
    }
}