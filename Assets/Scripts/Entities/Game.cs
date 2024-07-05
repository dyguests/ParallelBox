namespace Entities
{
    public interface IGame
    {
        IPlate Plate { get; }
    }

    public class Game : IGame
    {
        #region IGame

        public IPlate Plate { get; }

        #endregion

        #region Game

        public Game(IPlate plate)
        {
            Plate = plate;
        }

        #endregion
    }
}