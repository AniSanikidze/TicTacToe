using System;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            MainLoop();
        }

        static void MainLoop()
        {
            GameType gameType = TicTacToe.AskForGameType();
            int gridDimension = TicTacToe.GetGridDimension();
            var grid = TicTacToe.GenerateGrid(gridDimension);
            TicTacToe.CreateGamePlayers(gameType, out Player player1, out Player player2);
            Player currentPlayer = player1.Symbol == 'O' ? player1 : player2;
            Console.WriteLine("Initial grid: ");
            TicTacToe.PrintGrid(grid);
            do
            {
                currentPlayer = currentPlayer == player1 ? player2 : player1;
                try
                {
                    if (currentPlayer is ComputerPlayer)
                    {
                        grid = ((ComputerPlayer)currentPlayer).MakeRandomMove(grid);
                    }
                    else
                    {
                        grid = TicTacToe.GetPlayerMove(currentPlayer, grid);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TicTacToe.PrintGrid(grid);
                    grid = TicTacToe.GetPlayerMove(currentPlayer, grid);
                }
                TicTacToe.PrintGrid(grid);
            }

            while (TicTacToe.CheckGameCondition(grid, currentPlayer) == GameConditions.GameOn);

        }
    }


    public enum GameConditions
    {
        GameOn,
        GameWon,
        Tie
    }

    public enum PlayerType
    {
        ComputerPlayer,
        HumanPlayer
    }

    public enum GameType
    {
        HumanVSHuman,
        HumanVSComputer
    }
}

