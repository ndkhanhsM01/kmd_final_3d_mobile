using System.Collections;

namespace MLib
{
    [System.Serializable]
    public class LocalData
    {
        public int CurrentLevel;
        public float VolumeSound;
        public float VolumeMusic;

        public LocalData()
        {
            CurrentLevel = 0;
            VolumeSound = 1f;
            VolumeMusic = 1f;
        }
    }
}
