using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Commons;
using Koyou.Frameworks;
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

            // todo 取缓存 level index
            // todo 加载 Game
            var game = new Game(new Plate(5, 7));
            AppStateMachine.Instance.EnqueueState(new GameAppState(game));
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }

        #endregion
    }
}