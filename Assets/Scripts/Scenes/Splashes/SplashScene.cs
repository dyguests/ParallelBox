using Cysharp.Threading.Tasks;
using Koyou.Commons;
using Koyou.Frameworks;
using Repositories;
using Scenes.Games;

namespace Scenes.Splashes
{
    public class SplashScene : BaseScene
    {
        #region BaseScene

        public override async UniTask Enter()
        {
            await base.Enter();
            await UniTask.Delay(1000); // todo
            Log.N($"Called");

            var index = GamePrefs.CurrentLevelIndex;
            var game = GameDatas.GetLevel(index);
            AppStateMachine.Instance.EnqueueState(new GameAppState(game));
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }

        #endregion
    }
}