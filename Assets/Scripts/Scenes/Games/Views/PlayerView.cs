using Cysharp.Threading.Tasks;
using Entities;
using Scenes.Games.Views;
using UnityEngine;

namespace Scenes.Games
{
    public class PlayerView : PlacementView<IPlayer>
    {
        private static PlayerView sPrefab;

        public static async UniTask<PlayerView> Generate(IPlayer data, PlateView plateView)
        {
            if (sPrefab == null) sPrefab = Resources.Load<PlayerView>("Game/Player");
            var instantiate = Instantiate(sPrefab, plateView.transform);
            instantiate.name = $"Player{data.Pos}";
            instantiate.PlateView = plateView;
            await instantiate.LoadData(data);
            return instantiate;
        }
    }
}