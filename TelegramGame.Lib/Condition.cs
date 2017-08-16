namespace TelegramGame.Lib
{
    public class Condition
    {
        public enum ConditionVariant
        {
            LessThen,
            GreaterThenOrEqual
        }
        public Asset.AssetNames WhatToTest { get; set; }
        public ConditionVariant HowToTest { get; set; }
        public double TestValue { get; set; }

        public bool Passing(GameState gameState)
        {
            var passing = false;
            Asset assetToTest;
            gameState.Assets.TryGetValue(WhatToTest, out assetToTest);
            switch (HowToTest)
            {
                case ConditionVariant.LessThen:
                    passing = TestValue < assetToTest.Value;
                    break;
                case ConditionVariant.GreaterThenOrEqual:
                    passing = TestValue >= assetToTest.Value;
                    break;
                default:
                    break;
            }
            return passing;
        }
    }
}