using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Commons;
using Koyou.Frameworks;
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

            var plate = new Plate(5, 7);
            plate.Size.GetEnumerator().ForEach(pos => plate.Insert(pos, new Ground()));
            Game = new Game(plate);
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

        public Game Game { get; set; }

        #endregion
    }
}