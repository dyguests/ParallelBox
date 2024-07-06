using Cysharp.Threading.Tasks;
using Entities;
using Scenes.Games.Views;
using UnityEngine;

namespace Scenes.Games
{
    public class GoalView : PlacementView<IGoal>
    {
        private static GoalView sPrefab;

        public static async UniTask<GoalView> Generate(IGoal data, PlateView plateView)
        {
            if (sPrefab == null) sPrefab = Resources.Load<GoalView>("Game/Goal");
            var instantiate = Instantiate(sPrefab, plateView.transform);
            instantiate.name = $"Goal{data.Pos}";
            instantiate.PlateView = plateView;
            await instantiate.LoadData(data);
            return instantiate;
        }
    }
}