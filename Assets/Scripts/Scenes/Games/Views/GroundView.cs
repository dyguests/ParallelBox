using Cysharp.Threading.Tasks;
using Entities;
using UnityEngine;

namespace Scenes.Games
{
    public class GroundView : PlacementView<IGround>
    {
        private static GroundView sPrefab;

        public static async UniTask<GroundView> Generate(IGround data, PlateView plateView)
        {
            if (sPrefab == null) sPrefab = Resources.Load<GroundView>("Game/Ground");
            var instantiate = Instantiate(sPrefab, plateView.transform);
            instantiate.name = $"Ground{data.Pos}";
            instantiate.PlateView = plateView;
            await instantiate.LoadData(data);
            return instantiate;
        }
    }
}