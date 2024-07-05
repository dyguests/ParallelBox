using Cysharp.Threading.Tasks;
using Koyou.Frameworks;

namespace Scenes.Games
{
    public class GameScene : BaseScene
    {
        #region BaseScene

        public override async UniTask Enter()
        {
            await base.Enter();
        }

        public override async UniTask Exit()
        {
            await base.Exit();
        }

        #endregion
    }
}