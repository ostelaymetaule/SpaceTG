using System;

namespace TelegramGame.Lib
{
    public class Effects
    {
        public enum EffectVariant
        {
            Offset,
            Coeffitient,
            Magnitude
        }
        public Asset.AssetNames WhatChanges { get; set; }
        public EffectVariant HowChanges { get; set; }
        public double ChangeValue { get; set; }
        public bool ProcessChange(ref GameState gameState)
        {
            Asset asset;
            if (gameState.Assets.TryGetValue(WhatChanges, out asset))
            {
                switch (HowChanges)
                {
                    case EffectVariant.Offset:
                        asset.Value += ChangeValue;
                        break;
                    case EffectVariant.Coeffitient:
                        asset.Value *= ChangeValue;
                        break;
                    case EffectVariant.Magnitude:
                        asset.Value = Math.Pow(asset.Value, ChangeValue);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                asset = new Asset();
            }
            gameState.Assets[WhatChanges] = asset;

            return true;
        }
    }
}