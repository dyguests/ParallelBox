using Entities;
using Koyou.Commons;
using UnityEngine;

namespace Repositories
{
    /// <summary>
    ///  关卡数据
    /// </summary>
    public class GameDatas
    {
        public const int Count = 5;

        public static IGame GetLevel(int index)
        {
            return index switch
            {
                0 => GetLevel0(),
                _ => GetLevel0()
            };
        }

        private static IGame GetLevel0()
        {
            var plate = new Plate(7, 5);
            plate.Size.GetEnumerator().ForEach(pos => plate.Insert(pos, new Ground()));
            plate.Insert(new Vector2Int(1, 1), new Player());
            plate.Insert(new Vector2Int(2, 2), new Box());
            plate.Insert(new Vector2Int(3, 1), new Splitter(false, true, false, true));
            plate.Insert(new Vector2Int(3, 3), new Wall());
            plate.Insert(new Vector2Int(4, 2), new Goal());
            var game = new Game(plate);
            game.Record();

            return game;
        }
    }
}