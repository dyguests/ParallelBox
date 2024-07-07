using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Entities;
using Koyou.Frameworks;
using Scenes.Games.Views;
using UnityEngine;

namespace Scenes.Games
{
    public class PlateViewport : DataView<IPlate>
    {
        private static PlateViewport sPrefab;

        /// <summary>
        /// 仅用于在Hierarchy中区分不同的PlateViewport
        /// </summary>
        private static long sId;

        public static async UniTask<PlateViewport> Generate(IPlate data, GameView gameView)
        {
            if (sPrefab == null) sPrefab = Resources.Load<PlateViewport>("Game/PlateViewport");
            var instantiate = Instantiate(sPrefab);
            instantiate.name = $"PlateViewport{sId++}";
            instantiate.GameView = gameView;
            await instantiate.LoadData(data);
            return instantiate;
        }

        #region DataView<IPlate>

        public override async UniTask LoadData(IPlate data)
        {
            await base.LoadData(data);
            await plateView.LoadData(data);
        }

        public override async UniTask UnloadData()
        {
            await plateView.UnloadData();
            await base.UnloadData();
        }

        #endregion

        #region PlateViewport

        [SerializeField] private PlateView plateView;

        public GameView GameView { get; set; }

        public async Task Delete()
        {
            await UnloadData();
            GameView = null;
            Destroy(gameObject);
        }

        #endregion
    }
}