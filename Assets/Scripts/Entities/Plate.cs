using System.Collections.Generic;
using System.Linq;
using Koyou.Commons;
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

        public bool Contains(Vector2Int pos) => Size.Contains(pos);

        public void Insert(Vector2Int pos, IPlacement placement)
        {
            var cell = Cells.GetOrPut(pos, () => new Cell());
            var lastItem = cell.Get(placement.Layer);
            if (lastItem != null) Remove(pos, placement.Layer);
            cell.Set(placement);
            placement.Inserted(this, pos);
            // AddTransition(InsertTransition.From(pos, placement));
        }

        public void Remove(Vector2Int pos, int layer)
        {
            var cell = Cells.Get(pos);
            if (cell == null) return;
            var placement = cell.Get(layer);
            if (placement == null) return;
            cell.Remove(placement);
            placement.Removed();
            // AddTransition(RemoveTransition.From(pos, placement));
        }

        public IEnumerable<IPlacement> Get(Vector2Int pos) => Cells.Get(pos) ?? Enumerable.Empty<IPlacement>();
        public T Get<T>(Vector2Int pos) where T : IPlacement => Cells.Get(pos).OfType<T>().FirstOrDefault();

        #endregion

        #region Plate

        private ICell[,] Cells { get; }

        public Plate(int width, int height) : this(new Vector2Int(width, height)) { }

        public Plate(Vector2Int size)
        {
            Size = size;
            Cells = new ICell[size.x, size.y];
        }

        #endregion
    }
}