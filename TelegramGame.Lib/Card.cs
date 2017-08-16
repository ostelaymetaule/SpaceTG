using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramGame.Lib
{
    public class Card
    {
        public string Id { get; set; }
        public string PicUrl { get; set; }
        public string Text { get; set; }
        public string ShortDescription { get; set; }
        public Dictionary<Effects.EffectVariant, Effects> Effects { get; set; }
        public Dictionary<Condition.ConditionVariant, Condition> Conditions { get; set; }
        public List<Card> PossibleOutcomes { get; set; }
    }
}