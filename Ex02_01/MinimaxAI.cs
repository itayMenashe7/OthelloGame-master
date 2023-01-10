
namespace Ex02_Othelo
{
    using System;
    using static Ex02_Othelo.OtelloConstants;

    public class MinimaxAI
    {
        private readonly int r_Depth;
        private readonly char r_Player;

        public MinimaxAI(int depth, char playerSign)
        {
            this.r_Depth = depth;
            this.r_Player = playerSign;
        }

        public Move GetBestMove(OtehlloBoard board)
        {
            int bestScore = int.MinValue;
            Move bestMove = null;

            foreach (Move move in board.GetPossibleMoves(this.r_Player))
            {
                OtehlloBoard newBoard = board.GetCopyBoard(move, this.r_Player);
                int score = this.Minimax(newBoard, this.r_Depth - 1, this.getNextSign());
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        private int Minimax(OtehlloBoard io_board, int i_depth, char i_player)
        {
            if (i_depth == 0 || io_board.IsGameOver())
            {
                return io_board.Evaluate(this.r_Player);
            }

            int bestScore;
            if (i_player == this.r_Player)
            {
                bestScore = int.MinValue;
                foreach (var move in io_board.GetPossibleMoves(i_player))
                {
                    var newBoard = io_board.GetCopyBoard(move, i_player);
                    var score = this.Minimax(newBoard, i_depth - 1, this.getNextSign());
                    bestScore = Math.Max(bestScore, score);
                }
            }
            else
            {
                bestScore = int.MaxValue;
                foreach (var move in io_board.GetPossibleMoves(i_player))
                {
                    var newBoard = io_board.GetCopyBoard(move, i_player);
                    var score = this.Minimax(newBoard, i_depth - 1, this.getNextSign());
                    bestScore = Math.Min(bestScore, score);
                }
            }

            return bestScore;
        }

        private char getNextSign()
        {
            return (this.r_Player == k_FirstPlayerSign) ? k_SecondPlayerSign : k_FirstPlayerSign;
        }
    }
}