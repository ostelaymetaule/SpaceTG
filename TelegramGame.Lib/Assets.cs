using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramGame.Lib
{
    public class Asset
    {
        public enum AssetNames
        {
            Fuel,
            Water,
            Crew,
            RediationLevel
        }

        public AssetNames Name { get; set; }
        public double Value { get; set; }

    }
}
