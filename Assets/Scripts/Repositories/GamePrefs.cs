using UnityEngine;

namespace Repositories
{
    public static class GamePrefs
    {
        private const string KeyCurrentLevelIndex = "CurrentLevelIndex";

        public static int CurrentLevelIndex
        {
            get => PlayerPrefs.GetInt(KeyCurrentLevelIndex, 0);
            set => PlayerPrefs.SetInt(KeyCurrentLevelIndex, value);
        }
    }
}