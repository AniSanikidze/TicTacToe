using System;

namespace Tic_Tac_Toe
{
    static class TicTacToe
    {
        public static GameType AskForGameType()
        {
            Console.WriteLine("Please enter corresponding number of the game type you want to play:");
            Console.WriteLine("1. Human VS Human");
            Console.WriteLine("2. Human VS Computer");
            bool isValidInput = int.TryParse(Console.ReadLine(), out int userChoice);

            if (isValidInput && userChoice == 1 || userChoice == 2)
            {
                if (userChoice == 1)
                {
                    return GameType.HumanVSHuman;
                }
            }
            else
            {
                while (!isValidInput && userChoice != 1 && userChoice != 2)
                {
                    Console.WriteLine("Invalid input was provided.");
                    AskForGameType();
                }
            }
            return GameType.HumanVSComputer;
        }

        public static void CreateGamePlayers(GameType gameType, out Player player1, out Player player2)
        {
            Console.Write("Please enter the name for player 1: ");
            string player1Name = Console.ReadLine();
            bool isValidSymbol;
            int symbolIndex;
            do
            {
                Console.WriteLine("Please choose the symbol for player 1: ");
                Console.WriteLine("1: X");
                Console.WriteLine("2: O");
                isValidSymbol = int.TryParse(Console.ReadLine(), out symbolIndex);
                if (!isValidSymbol)
                {
                    Console.WriteLine("Invalid choice was made");
                }
            }
            while (!isValidSymbol && symbolIndex != 1 && symbolIndex != 2);

            char player1Symbol = symbolIndex == 1 ? 'X' : 'O';
            player1 = new HumanPlayer(player1Symbol, player1Name);
            char player2Symbol = player1Symbol == 'X' ? 'O' : 'X';

            if (gameType == GameType.HumanVSHuman)
            {
                Console.WriteLine("Please enter the name for player 2: ");
                string player2Name = Console.ReadLine();
                Console.WriteLine($"Player 2 was automatically assigned with the symbol - {player2Symbol}");
                player2 = new HumanPlayer(player2Symbol, player2Name);
            }
            else
            {
                player2 = new ComputerPlayer(player2Symbol);
                Console.WriteLine($"Computer Player was assigned with symbol - {player2Symbol}");
            }
        }

        public static int GetGridDimension()
        {
            bool isValidInput;
            int dimension;
            do
            {
                Console.WriteLine("Please enter the grid dimension: ");
                isValidInput = int.TryParse(Console.ReadLine(), out dimension);
                if (!isValidInput || dimension < 0)
                {
                    Console.WriteLine("Grid dimension should be greater than 0.");
                }
            }
            while (!isValidInput || dimension < 0);


            return dimension;
        }

        public static char[,] GenerateGrid(int n)
        {
            char[,] grid = new char[n, n];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int a = 0; a < grid.GetLength(1); a++)
                {
                    grid[i, a] = ' ';
                }
            }
            return grid;
        }
        public static void PrintGrid(char[,] grid)
        {
            Console.Write("   ");
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (i == 0)
                {
                    for (int z = 0; z < grid.GetLength(1); z++)
                    {
                        Console.Write(z + " ");
                    }
                }
                Console.WriteLine();
                Console.Write(i + " ");

                for (int a = 0; a < grid.GetLength(1); a++)
                {
                    if (a == grid.GetLength(1) - 1)
                    {
                        Console.Write($"|{grid[i, a]}|\n");
                    }
                    else
                    {
                        Console.Write($"|{grid[i, a]}");
                    }
                }
                Console.WriteLine();
            };
        }

        public static char[,] GetPlayerMove(Player player, char[,] grid)
        {
            Console.Write($"It is {player.Name}'s turn. Please enter row and column nums: ");
            string userInput = Console.ReadLine();
            bool IsValidRowNum = int.TryParse(userInput[0].ToString(), out int rowNum);
            bool IsValidColNum = int.TryParse(userInput[1].ToString(), out int colNum);

            if (IsValidColNum && IsValidRowNum && rowNum >= 0 && rowNum < grid.GetLength(0) && colNum >= 0 && colNum < grid.GetLength(1))
            {
                var squareToFill = grid[rowNum, colNum];
                if (squareToFill != 'X' && squareToFill != 'O')
                {
                    grid[rowNum, colNum] = player.Symbol;
                }
                else
                {
                    throw new Exception("Provided input square is already filled. Please choose an empty square.");
                }

            }
            else
            {
                throw new Exception("Invalid row and column numbers were provided.");
            }

            return grid;
        }

        public static GameConditions CheckGameCondition(char[,] grid, Player player)
        {
            if (CheckHorizontalLines(grid) || CheckVerticalLines(grid) || CheckLeftDiagonal(grid) || CheckRightDiagonal(grid))
            {
                Console.WriteLine(player.Name + " won the game!");
                return GameConditions.GameWon;
            }
            else if (CheckIfGridIsFull(grid))
            {
                Console.WriteLine("Game is tie!");
                return GameConditions.Tie;
            }
            else
            {
                return GameConditions.GameOn;
            }
        }

        private static bool CheckIfGridIsFull(char[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int a = 0; a < grid.GetLength(1); a++)
                {
                    if (grid[i, a] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private static bool CheckHorizontalLines(char[,] grid)
        {
            bool horizontalWon = false;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int a = 0; a < grid.GetLength(1); a++)
                {
                    if (grid[i, 0] != grid[i, a] || grid[i, a] == (char)' ')
                    {
                        horizontalWon = false;
                        break;
                    }
                    if ((i == grid.GetLength(0) - 1) && grid[i, a] == (char)' ')
                    {
                        return false;
                    }
                    horizontalWon = true;
                    if (horizontalWon && a == grid.GetLength(1) - 1 && grid[i, a] != (char)' ')
                    {
                        return true;
                    }
                }
            }
            return horizontalWon;
        }
        public static bool CheckVerticalLines(char[,] grid)
        {
            bool verticalWon = false;
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                char lineItem = grid[i, i];
                if (lineItem == (char)' ' && i == grid.GetLength(1) - 1) return false;
                for (int a = 0; a < grid.GetLength(0); a++)
                {
                    if (lineItem != grid[a, i] || grid[a, i] == (char)' ')
                    {
                        verticalWon = false;
                        break;
                    }
                    if ((a == grid.GetLength(0) - 1) && grid[a, i] == (char)' ')
                    {
                        return false;
                    }
                    verticalWon = true;
                    if (verticalWon && a == grid.GetLength(1) - 1 && grid[a, i] != (char)' ')
                    {
                        return true;
                    }
                }
            }
            return verticalWon;
        }
        public static bool CheckRightDiagonal(char[,] grid)
        {
            bool rightDiagonalWon = false;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                char lineItem = grid[0, grid.GetLength(0) - 1];
                if (lineItem == (char)' ' && i != grid.GetLength(0) - 1) return false;
                if (lineItem != grid[grid.GetLength(0) - 1 - i, i])
                {
                    rightDiagonalWon = false;
                    return rightDiagonalWon;
                }
                rightDiagonalWon = true;
                if (rightDiagonalWon && i == grid.GetLength(0) - 1)
                {
                    return true;
                }
            }
            return rightDiagonalWon;
        }
        public static bool CheckLeftDiagonal(char[,] grid)
        {
            bool leftDiagonalWon = false;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                char lineItem = grid[0, 0];
                if (lineItem == (char)' ' && i == grid.GetLength(0) - 1) return false;
                if (lineItem != grid[grid.GetLength(0) - 1 - i, grid.GetLength(0) - 1 - i])
                {
                    leftDiagonalWon = false;
                    return leftDiagonalWon;
                }
                leftDiagonalWon = true;
                if (leftDiagonalWon && i == grid.GetLength(0) - 1)
                {
                    return true;
                }
            }
            return leftDiagonalWon;
        }
    }
}
