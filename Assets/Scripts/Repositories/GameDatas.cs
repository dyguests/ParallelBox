using Entities;
using Koyou.Commons;
using UnityEngine;

namespace Repositories
{
    /// <summary>
    ///  关卡数据
    /// </summary>
    public static class GameDatas
    {
        public const int Count = 3;

        public static IGame GetLevel(int index)
        {
            return index switch
            {
                0 => GetLevel0(),
                1 => GetLevel1(),
                2 => GetLevel2(),
                _ => GetLevel0()
            };
        }

        private static IGame GetLevel0()
        {
            var plate = new Plate(7, 5);
            plate.Size.GetEnumerator().ForEach(pos => plate.Insert(pos, new Ground()));
            plate.Insert(new Vector2Int(1, 1), new Player());
            plate.Insert(new Vector2Int(2, 2), new Box());
            plate.Insert(new Vector2Int(3, 1), new Wall());
            plate.Insert(new Vector2Int(3, 2), new Wall());
            plate.Insert(new Vector2Int(5, 2), new Goal());
            var game = new Game(plate);
            game.Record();

            return game;
        }

        private static IGame GetLevel1()
        {
            var plate = new Plate(7, 5);
            plate.Size.GetEnumerator().ForEach(pos => plate.Insert(pos, new Ground()));
            plate.Insert(new Vector2Int(1, 1), new Player());
            plate.Insert(new Vector2Int(2, 2), new Box(new Ratio(1, 1, false)));
            plate.Insert(new Vector2Int(3, 2), new Splitter(false, true, false, true));
            plate.Insert(new Vector2Int(4, 2), new Wall());
            plate.Insert(new Vector2Int(5, 2), new Goal());
            var game = new Game(plate);
            game.Record();

            return game;
        }

        private static IGame GetLevel2()
        {
            var plate = new Plate(7, 5);
            plate.Size.GetEnumerator().ForEach(pos => plate.Insert(pos, new Ground()));
            plate.Insert(new Vector2Int(1, 1), new Player(new Ratio(1, 1, false)));
            plate.Insert(new Vector2Int(1, 2), new Box(new Ratio(1, 1, false)));
            plate.Insert(new Vector2Int(1, 3), new Splitter(true, false, true, false));
            plate.Insert(new Vector2Int(4, 2), new Ground(new Ratio(1, 2, true)));
            plate.Insert(new Vector2Int(5, 1), new Wall());
            plate.Insert(new Vector2Int(5, 3), new Wall());
            plate.Insert(new Vector2Int(6, 0), new Wall());
            plate.Insert(new Vector2Int(6, 4), new Wall());
            plate.Insert(new Vector2Int(5, 2), new Goal());
            var game = new Game(plate);
            game.Record();

            return game;
        }
    }
}