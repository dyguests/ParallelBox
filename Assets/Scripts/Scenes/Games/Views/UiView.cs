using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Frameworks;
using Koyou.Recordables;
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

        private IDisperser _disperser;

        public override async UniTask LoadData(IGame data)
        {
            await base.LoadData(data);
            _disperser = Data.Collect<IGame>(ApplyChange);
        }

        public override async UniTask UnloadData()
        {
            _disperser.Disperse();
            await base.UnloadData();
        }

        #endregion

        #region UiView

        [SerializeField] private GameObject winGo;

        private void ApplyChange(IGame previous, IGame current, List<ITransition> transitions)
        {
            if (transitions == null) return;
            var hasCompleted = transitions.Any(transition => transition is Game.CompletedTransition);
            if (!hasCompleted) return;
            winGo.SetActive(true);
        }

        #endregion
    }
}