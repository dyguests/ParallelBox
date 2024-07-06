using Entities;
using Koyou.Frameworks;
using UnityEngine;

namespace Scenes.Games
{
    public class GameView : DataView<IGame>
    {
        #region GameView

        [SerializeField] private PlateView plateView;

        #endregion
    }
}