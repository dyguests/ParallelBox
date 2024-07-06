using Cysharp.Threading.Tasks;
using Entities;
using Scenes.Games.Views;
using UnityEngine;

namespace Scenes.Games
{
    public class BoxView : PlacementView<IBox>
    {
        private static BoxView sPrefab;

        public static async UniTask<BoxView> Generate(IBox data, PlateView plateView)
        {
            if (sPrefab == null) sPrefab = Resources.Load<BoxView>("Game/Box");
            var instantiate = Instantiate(sPrefab, plateView.transform);
            instantiate.name = $"Box";
            instantiate.PlateView = plateView;
            await instantiate.LoadData(data);
            return instantiate;
        }
    }
}