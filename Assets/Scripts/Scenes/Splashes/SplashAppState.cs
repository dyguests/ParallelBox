using Cysharp.Threading.Tasks;
using Koyou.Frameworks;

namespace Scenes.Splashes
{
    public class SplashAppState : AppState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }
    }
}