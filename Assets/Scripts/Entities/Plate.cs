using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public interface IPlate
    {
        Vector2Int Size { get; }

        bool Contains(Vector2Int pos);
        void Insert(Vector2Int pos, IPlacement placement);
        void Remove(Vector2Int pos, int layer);

        IEnumerable<IPlacement> Get(Vector2Int pos);
        T Get<T>(Vector2Int pos) where T : IPlacement;
    }

    public class Plate : IPlate
    {
        #region IPlate

        public Vector2Int Size { get; }

        public bool Contains(Vector2Int pos)
        {
            throw new NotImplementedException();
        }

        public void Insert(Vector2Int pos, IPlacement placement)
        {
            throw new NotImplementedException();
        }

        public void Remove(Vector2Int pos, int layer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPlacement> Get(Vector2Int pos)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(Vector2Int pos) where T : IPlacement
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Plate

        private ICell[,] Cells { get; }

        #endregion
    }
}