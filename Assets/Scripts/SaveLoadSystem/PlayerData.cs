using System.Collections.Generic;

namespace GameData
{
    [System.Serializable]
    public class PlayerData
    {
        public int coins = 0;
        public int record = 0;
        public SkinType selectedSkin = SkinType.ORANGE;

        public List<SkinType> avaliableSkins = new List<SkinType>() { SkinType.ORANGE };
    }
}

