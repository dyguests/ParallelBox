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
        public static long Id;

        public static async UniTask<PlateViewport> Generate(IPlate data, GameView gameView)
        {
            if (sPrefab == null) sPrefab = Resources.Load<PlateViewport>("Game/PlateViewport");
            var instantiate = Instantiate(
                sPrefab,
                // ReSharper disable once PossibleLossOfFraction
                new Vector3((Id % 255) * 100, (Id / 255) * 100, 0),
                Quaternion.identity
            );
            instantiate.name = $"PlateViewport{Id++}";
            instantiate.GameView = gameView;
            await instantiate.LoadData(data);
            return instantiate;
        }

        #region DataView<IPlate>

        public override async UniTask LoadData(IPlate data)
        {
            await base.LoadData(data);
            // todo camera scale
            await plateView.LoadData(data);
        }

        public override async UniTask UnloadData()
        {
            await plateView.UnloadData();
            await base.UnloadData();
        }

        #endregion

        #region PlateViewport

        [SerializeField] private new Camera camera;
        [SerializeField] private PlateView plateView;

        public GameView GameView { get; set; }

        public async Task Delete()
        {
            await UnloadData();
            GameView = null;
            Destroy(gameObject);
        }

        public async UniTask RearrangeViewport(int index, int count)
        {
            Debug.Assert(count > 0);

            if (count == 1)
            {
                camera.rect = new Rect(0, 0, 1, 1);
                return;
            }

            // 计算行列数
            int rows = Mathf.CeilToInt(Mathf.Sqrt(count));
            int cols = Mathf.CeilToInt((float)count / rows);

            // 计算每个视口的宽度和高度
            float width = 1f / cols;
            float height = 1f / rows;

            // 计算当前视口的行和列位置
            int row = index / cols;
            int col = index % cols;

            // 设置摄像机的视口
            camera.rect = new Rect(col * width, 1 - (row + 1) * height, width, height);
        }

        #endregion
    }
}