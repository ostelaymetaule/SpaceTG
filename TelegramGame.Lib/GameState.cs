using System.Collections.Generic;

namespace TelegramGame.Lib
{
    public class GameState
    {
        public Dictionary<Asset.AssetNames, Asset> Assets { get; set; }
        public string UserId { get; set; }
        public List<Card> PlayableCards { get; set; }

    }
}
