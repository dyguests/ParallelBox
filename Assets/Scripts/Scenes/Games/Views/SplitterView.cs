using Cysharp.Threading.Tasks;
using Entities;
using Scenes.Games.Views;
using UnityEngine;

namespace Scenes.Games
{
    public class SplitterView : PlacementView<ISplitter>
    {
        private static SplitterView sPrefab;

        public static async UniTask<SplitterView> Generate(ISplitter data, PlateView plateView)
        {
            if (sPrefab == null) sPrefab = Resources.Load<SplitterView>("Game/Splitter");
            var instantiate = Instantiate(sPrefab, plateView.transform);
            instantiate.name = $"Splitter{data.Pos}";
            instantiate.PlateView = plateView;
            await instantiate.LoadData(data);
            return instantiate;
        }

        #region MonoBehaviour

        private void Awake()
        {
            leftGo.SetActive(false);
            upGo.SetActive(false);
            rightGo.SetActive(false);
            downGo.SetActive(false);
        }

        #endregion

        #region PlacementView<ISplitter>

        public override async UniTask LoadData(ISplitter data)
        {
            await base.LoadData(data);
            leftGo.SetActive(data.Left);
            upGo.SetActive(data.Up);
            rightGo.SetActive(data.Right);
            downGo.SetActive(data.Down);
        }

        public override async UniTask UnloadData()
        {
            await base.UnloadData();
        }

        #endregion

        #region SplitterView

        [SerializeField] private GameObject leftGo;
        [SerializeField] private GameObject upGo;
        [SerializeField] private GameObject rightGo;
        [SerializeField] private GameObject downGo;

        #endregion
    }
}