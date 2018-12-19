using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Core
{
    public class Card
    {
        public bool Hidden { get; set;} = true;
        public Suit Suit { get; set; }
        public Face Face { get; set; }
        public int Value { get; set; }
    }
}
