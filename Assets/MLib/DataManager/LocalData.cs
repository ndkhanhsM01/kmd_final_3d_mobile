using System.Collections;
using System.Collections.Generic;

namespace MLib
{
    [System.Serializable]
    public class LocalData
    {
        public bool IsFirsTimePlay;
        public int CurrentLevel;
        public float VolumeSound;
        public float VolumeMusic;

        public int Coin;
        public int SkinSelected;
        public long LastTimeLogin;
        public HashSet<int> SkinsUnlocked;
        public HashSet<int> CoinsCollected;

        public LocalData()
        {
            IsFirsTimePlay = true;
            CurrentLevel = 0;
            VolumeSound = 1f;
            VolumeMusic = 1f;
            Coin = 0;
            SkinSelected = MConstraint.DefaultSkin;
            SkinsUnlocked = new HashSet<int>() { MConstraint.DefaultSkin};
            CoinsCollected = new();
        }
    }
}
