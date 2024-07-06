using System.Linq;
using Koyou.Commons;
using UnityEngine;

namespace Entities
{
    public interface IGame
    {
        IPlate Plate { get; }
        IControllable Controllable { get; }

        bool Move(Vector2Int direction);
    }

    public class Game : IGame
    {
        #region IGame

        public IPlate Plate { get; }
        public IControllable Controllable { get; }

        public bool Move(Vector2Int direction)
        {
            return false;
        }

        #endregion

        #region Game

        public Game(IPlate plate)
        {
            Plate = plate;
            Controllable = Plate.Size.GetEnumerator().SelectMany(Plate.Get).OfType<IControllable>().FirstOrDefault().RequireNotNull();
        }

        #endregion
    }
}