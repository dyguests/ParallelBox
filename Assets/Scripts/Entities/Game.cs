using Koyou.Recordables;
using UnityEngine;

namespace Entities
{
    public interface IGame : IRecordable
    {
        IPlate Plate { get; }

        bool Move(Vector2Int direction);
    }

    public class Game : RecordableObject, IGame
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Game>(
            null,
            source => new Game(source.Plate),
            (source, target) => { },
            source => new[] { source.Plate });

        #endregion

        #region IGame

        // todo 平等世界 plates，后续还要再套一层
        public IPlate Plate { get; }

        public bool Move(Vector2Int direction)
        {
            var moved = Plate.Move(direction);

            if (!moved) return false;

            // todo completed check

            Record();

            return true;
        }

        #endregion

        #region Game

        public Game(IPlate plate)
        {
            Plate = plate;
        }

        #endregion
    }
}