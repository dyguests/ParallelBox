using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Entities;
using JetBrains.Annotations;
using Koyou.Commons;
using Koyou.Frameworks;

namespace Scenes.Games
{
    public class PlateView : DataView<IPlate>
    {
        #region DataView<IPlate>

        public override async UniTask LoadData(IPlate data)
        {
            await base.LoadData(data);
            await Data.Size.GetEnumerator()
                .Select(pos => Data.Get(pos))
                .Where(Predicates.NotNull)
                .SelectMany(placements => placements)
                .Select(async placement => await InsertItemView(placement));
        }

        public override async UniTask UnloadData()
        {
            await Data.Size.GetEnumerator()
                .Select(pos => Data.Get(pos))
                .Where(Predicates.NotNull)
                .SelectMany(placements => placements)
                .Select(async placement => await RemoveItemView(placement));
            await base.UnloadData();
        }

        #endregion

        #region PlateView

        private readonly Dictionary<IPlacement, IPlacementView> mPlacementViews = new();


        private async UniTask InsertItemView([NotNull] IPlacement placement)
        {
            switch (placement)
            {
                case IGround ground:
                {
                    var placementView = await GroundView.Generate(ground, this);
                    mPlacementViews.Add(placement, placementView);
                    break;
                }
                default: throw new InvalidDataException();
            }
        }

        private async UniTask RemoveItemView([NotNull] IPlacement placement)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}