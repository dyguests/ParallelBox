using System.Collections.Generic;
using Koyou.Recordables;
using UnityEngine;

namespace Entities
{
    public interface IGame : IRecordable
    {
        List<IPlate> Plates { get; }

        bool Move(Vector2Int direction);
    }

    public class Game : RecordableObject, IGame
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Game>(
            null,
            source => new Game(),
            (source, target) => { target.Plates.AddRange(source.Plates); },
            source => source.Plates
        );

        #endregion

        #region IGame

        public List<IPlate> Plates { get; } = new List<IPlate>();

        public bool Move(Vector2Int direction)
        {
            var anyMoved = false;
            foreach (var plate in Plates)
            {
                var moved = plate.Move(direction, out var splittingMovements);

                if (!moved) continue;
                anyMoved = true;

                // split
                foreach (var movement in splittingMovements)
                {
                    var splitPlates = plate.Split(movement);

                    // todo 添加多个，还有UI
                    // todo 添加多个，还有UI
                    // todo 添加多个，还有UI
                    // todo 添加多个，还有UI
                }
            }

            if (!anyMoved) return false;

            // todo completed check

            Record();

            return true;
        }

        #endregion

        #region Game

        public Game(IPlate plate)
        {
            Plates.Add(plate);
        }

        private Game() { }

        #endregion
    }
}