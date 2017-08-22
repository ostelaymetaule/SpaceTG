using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramGame.Lib
{
    public class Card
    {
        public string PicUrl { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        
        public List<string> Transitions { get; set; }
    }
}