using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Entities;
using JetBrains.Annotations;
using Koyou.Commons;
using Koyou.Frameworks;
using UnityEngine;

namespace Scenes.Games.Views
{
    public class PlateView : DataView<IPlate>
    {
        #region DataView<IPlate>

        public override async UniTask LoadData(IPlate data)
        {
            await base.LoadData(data);
            // _pos2LocalMatrix = Matrix4x4.Translate(new Vector3(-(data.Size.x - 1) / 2f, -(data.Size.y - 1) / 2f));
            _pos2LocalMatrix = Matrix4x4.TRS(
                new Vector3(-(data.Size.x - 1) / 2f, -(data.Size.y - 1) / 2f, 0f),
                Quaternion.identity,
                new Vector3(tileSize.x, tileSize.y, 1)
            );
            await Data.Size.GetEnumerator()
                .Select(pos => Data.Get(pos))
                .Where(Predicates.NotNull)
                .SelectMany(placements => placements)
                .OrderBy(placement => placement.Layer)
                .Select(async placement => await InsertItemView(placement));
        }

        public override async UniTask UnloadData()
        {
            await Data.Size.GetEnumerator()
                .Select(pos => Data.Get(pos))
                .Where(Predicates.NotNull)
                .SelectMany(placements => placements)
                .Select(async placement => await RemoveItemView(placement));
            _pos2LocalMatrix = default;
            await base.UnloadData();
        }

        #endregion

        #region PlateView

        [SerializeField] private Vector2 tileSize = new Vector2(1f, 12f / 16f);

        private readonly Dictionary<IPlacement, IPlacementView> mPlacementViews = new();

        private async UniTask InsertItemView([NotNull] IPlacement placement)
        {
            switch (placement)
            {
                case IBox box:
                {
                    var placementView = await BoxView.Generate(box, this);
                    mPlacementViews.Add(placement, placementView);
                    break;
                }
                case IGoal goal:
                {
                    var placementView = await GoalView.Generate(goal, this);
                    mPlacementViews.Add(placement, placementView);
                    break;
                }
                case IGround ground:
                {
                    var placementView = await GroundView.Generate(ground, this);
                    mPlacementViews.Add(placement, placementView);
                    break;
                }
                case IPlayer player:
                {
                    var placementView = await PlayerView.Generate(player, this);
                    mPlacementViews.Add(placement, placementView);
                    break;
                }
                case ISplitter splitter:
                {
                    var placementView = await SplitterView.Generate(splitter, this);
                    mPlacementViews.Add(placement, placementView);
                    break;
                }
                case IWall wall:
                {
                    var placementView = await WallView.Generate(wall, this);
                    mPlacementViews.Add(placement, placementView);
                    break;
                }
                default: throw new InvalidDataException();
            }
        }

        private async UniTask RemoveItemView([NotNull] IPlacement placement)
        {
            var placementView = mPlacementViews[placement];
            mPlacementViews.Remove(placement);
            await placementView.Delete();
        }

        private Matrix4x4 _pos2LocalMatrix;

        public Vector3 Pos2Local(IPlacement placement) => _pos2LocalMatrix.MultiplyPoint((Vector2)placement.Pos);

        #endregion
    }
}