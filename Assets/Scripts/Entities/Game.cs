using System.Collections.Generic;
using System.Linq;
using Koyou.Commons;
using UnityEngine;

namespace Entities
{
    // todo recordable
    public interface IGame
    {
        IPlate Plate { get; }

        bool Move(Vector2Int direction);
    }

    public class Game : IGame
    {
        #region IGame
    
        // todo 平等世界 plates，后续还要再套一层
        public IPlate Plate { get; }

        public bool Move(Vector2Int direction)
        {
            var movements = new List<IMovement> { Plate.Controllable };

            while (true)
            {
                var nextPoses = movements.Select(movement => movement.Pos)
                    .Select(pos => pos + direction)
                    .ToList();
                if (nextPoses.Any(pos => !Plate.Contains(pos)))
                {
                    return false;
                }

                var collides = nextPoses
                    .Select(nextPos => Plate.Get(nextPos))
                    .Where(Predicates.NotNull)
                    .SelectMany(placements => placements)
                    .Where(placement => placement.Layer == Plate.Controllable.Layer) // 仅检查同层碰撞
                    .Where(placement => !movements.Contains(placement))
                    .ToList();
                if (!collides.Any())
                {
                    foreach (var movement in movements)
                    {
                        Plate.Move(movement.Pos, movement.Pos + direction, movement);
                    }

                    break;
                }

                if (collides.Any(placement => placement is not IMovement))
                {
                    return false;
                }

                movements.AddRange(collides.Cast<IMovement>());
            }

            // todo completed check

            Plate.Record();

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