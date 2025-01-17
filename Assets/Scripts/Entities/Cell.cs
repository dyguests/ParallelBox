﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Koyou.Commons;
using Koyou.Recordables;
using static Koyou.Commons.RequireEx;

namespace Entities
{
    public interface ICell : IRecordable, IEnumerable<IPlacement>, ICloneable<ICell>, ISplitCloneable<ICell>
    {
        void Set(IPlacement placement);
        void Remove(IPlacement placement);

        T Get<T>() where T : IPlacement;
        IPlacement Get(int layer);

        bool Has(int layer);

        void Splitted(int count);
    }

    public class Cell : RecordableObject, ICell
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Cell>(
            source => new Cell(),
            (source, target) => { target._map = source._map?.Let(map => new Dictionary<int, IPlacement>(map)); },
            source => source._map?.Values
        );

        #endregion

        #region ICell

        public void Set(IPlacement placement) => RequireOrNew(ref _map)[placement.Layer] = placement;

        public void Remove(IPlacement placement)
        {
            if (placement != null && _map?.GetValueOrDefault(placement.Layer) == placement)
            {
                _map?.Remove(placement.Layer);
            }
        }

        public T Get<T>()
            where T : IPlacement =>
            RequireOrNew(ref _map).Values.OfType<T>().FirstOrDefault();

        public IPlacement Get(int layer) => _map?.GetValueOrDefault(layer);

        public bool Has(int layer) => RequireOrNew(ref _map).ContainsKey(layer);

        public void Splitted(int count)
        {
            _map?.Values.ForEach(placement => placement.Splitted(count));
        }

        #endregion

        #region IEnumerable<IPlacement>

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<IPlacement> GetEnumerator() => _map?.Values.GetEnumerator()
                                                          ?? new Dictionary<int, IPlacement>.ValueCollection.Enumerator();

        #endregion

        #region ICloneable<ICell>

        public ICell Clone()
        {
            var clone = new Cell();
            if (_map != null)
            {
                clone._map = new Dictionary<int, IPlacement>();
                foreach (var key in _map.Keys)
                {
                    clone._map[key] = _map[key];
                }
            }

            return clone;
        }

        #endregion

        #region IDeepCloneable<ICell>

        public ICell SplitClone(int count)
        {
            var clone = new Cell();
            if (_map != null)
            {
                clone._map = new Dictionary<int, IPlacement>();
                foreach (var key in _map.Keys)
                {
                    clone._map[key] = _map[key].SplitClone(count);
                }
            }

            return clone;
        }

        #endregion

        #region Cell

        /// <summary>
        /// key: layer
        /// value: placement
        /// </summary>
        [CanBeNull] private Dictionary<int, IPlacement> _map;

        #endregion
    }
}