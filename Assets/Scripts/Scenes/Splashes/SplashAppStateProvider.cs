using Koyou.Frameworks;

namespace Scenes.Splashes
{
    public class SplashAppStateProvider : AppStateProvider
    {
        public override IAppState GetState()
        {
            return new SplashAppState();
        }
    }
}