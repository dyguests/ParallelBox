using Cysharp.Threading.Tasks;
using Entities;
using Scenes.Games.Views;
using UnityEngine;

namespace Scenes.Games
{
    public class WallView : PlacementView<IWall>
    {
        private static WallView sPrefab;

        public static async UniTask<WallView> Generate(IWall data, PlateView plateView)
        {
            if (sPrefab == null) sPrefab = Resources.Load<WallView>("Game/Wall");
            var instantiate = Instantiate(sPrefab, plateView.transform);
            instantiate.name = $"Wall{data.Pos}";
            instantiate.PlateView = plateView;
            await instantiate.LoadData(data);
            return instantiate;
        }
    }
}