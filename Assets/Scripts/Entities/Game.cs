using System.Collections.Generic;
using System.Linq;
using Koyou.Commons;
using Koyou.Recordables;
using UnityEngine;

namespace Entities
{
    public interface IGame : IRecordable
    {
        bool IsCompleted { get; }

        List<IPlate> Plates { get; }

        bool Move(Vector2Int direction);
    }

    public class Game : RecordableObject, IGame
    {
        #region RecordableObject

        protected override ISaver Savior { get; } = new Saver<Game>(
            null,
            source => new Game(),
            (source, target) =>
            {
                ((Game)target).IsCompleted = source.IsCompleted;
                target.Plates.AddRange(source.Plates);
            },
            source => source.Plates
        );

        #endregion

        #region IGame

        public bool IsCompleted { get; private set; }
        public List<IPlate> Plates { get; } = new();

        public bool Move(Vector2Int direction)
        {
            // 若已通关，禁用操作
            if (IsCompleted) return false;

            var anyMoved = false;
            foreach (var plate in Plates.ToArray())
            {
                var moved = plate.Move(direction, out var splittingMovements);

                if (!moved) continue;
                anyMoved = true;

                // split
                foreach (var movement in splittingMovements)
                {
                    var splitPlates = plate.Split(movement);
                    if (splitPlates.Any())
                    {
                        Plates.AddRange(splitPlates);
                        AddTransition(new AddPlatesTransition(splitPlates));
                    }

                    // todo 递归： 1分2，2分4，4分8
                }
            }

            if (!anyMoved) return false;

            var plates = Plates.ToArray();

            if (plates.Any(plate => plate.IsCompleted))
            {
                IsCompleted = true;
                AddTransition(CompletedTransition.Instance);
            }

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

        public class AddPlatesTransition : ITransition
        {
            public List<IPlate> Plates { get; }

            public AddPlatesTransition(List<IPlate> plates)
            {
                Plates = plates;
            }
        }

        public class CompletedTransition : Singleton<CompletedTransition>, ITransition { }

        #endregion
    }
}