using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramGame.Lib
{
    public class Transition
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public List<Effects> Effects { get; set; }
        public List<Condition> Conditions { get; set; }

        public List<string> PossibleCardNames { get; set; }



    }

}