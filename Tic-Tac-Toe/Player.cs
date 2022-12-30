using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe
{
    class Player
    {
        public char Symbol { get; private set; }

        public string Name { get; private set; }
        public Player(char symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }
    }
}
