using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Frameworks;
using UnityEngine;

namespace Scenes.Games.Views
{
    public class UiView : DataView<IGame>
    {
        #region MonoBehaviour

        private void Awake()
        {
            winGo.SetActive(false);
        }

        #endregion

        #region DataView<IGame>

        public override async UniTask LoadData(IGame data)
        {
            await base.LoadData(data);
        }

        public override async UniTask UnloadData()
        {
            await base.UnloadData();
        }

        #endregion

        #region UiView

        [SerializeField] private GameObject winGo;

        #endregion
    }
}