using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe
{
    class ComputerPlayer : Player
    {
        public ComputerPlayer(char symbol, string name = "Computer Player") : base(symbol, name)
        {
        }

        public char[,] MakeRandomMove(char[,] grid)
        {
            Random randomNum = new Random();
            char squareToFill;
            int rowNum;
            int colNum;
            do
            {
                rowNum = randomNum.Next(0, grid.GetLength(0));
                colNum = randomNum.Next(0, grid.GetLength(0));
                squareToFill = grid[rowNum, colNum];
                if (squareToFill != 'X' && squareToFill != 'O')
                {
                    grid[rowNum, colNum] = base.Symbol;
                }
            }
            while (squareToFill == 'X' || squareToFill == 'O');
            Console.WriteLine($"Computer player made a random move with the square row number of {rowNum} and column number - {colNum}");
            return grid;
        }
    }
}
