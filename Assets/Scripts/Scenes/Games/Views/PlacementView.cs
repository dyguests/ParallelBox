﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities;
using JetBrains.Annotations;
using Koyou.Frameworks;
using Koyou.Recordables;
using Scenes.Games.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Games
{
    public interface IPlacementView
    {
        UniTask Delete();
    }

    public class PlacementView<TData> : DataView<TData>, IPlacementView
        where TData : IPlacement
    {
        #region DataView<TData>

        private IDisperser _disperser;

        public override async UniTask LoadData(TData data)
        {
            await base.LoadData(data);
            _disperser = Data.Collect<TData>(ApplyChange);
        }

        public override async UniTask UnloadData()
        {
            _disperser.Disperse();
            await base.UnloadData();
        }

        #endregion

        #region IPlacementView

        public async UniTask Delete()
        {
            await UnloadData();
            PlateView = null;
            Destroy(gameObject);
        }

        #endregion

        #region PlacementView<TData>

        [SerializeField] [CanBeNull] private SpriteRenderer sr;

        [Space] [SerializeField] [CanBeNull] private Text ratioTExt;

        protected PlateView PlateView { get; set; }

        private void ApplyChange(TData previous, TData current, List<ITransition> transitions)
        {
            var localPosition = PlateView.Pos2Local(current);
            if (sr != null)
            {
                sr.sortingOrder = Mathf.CeilToInt(-localPosition.y * 10);
            }

            if (ratioTExt != null)
            {
                var ratio = current.Ratio;
                if (Math.Abs(ratio.Value - 1) < 0.001f)
                {
                    ratioTExt.enabled = false;
                }
                else
                {
                    ratioTExt.text = $"{ratio.molecule}/{ratio.letter}";
                    ratioTExt.enabled = true;
                }
            }

            // todo 动画
            transform.localPosition = localPosition;
        }

        #endregion
    }
}