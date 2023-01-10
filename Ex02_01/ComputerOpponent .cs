
namespace Ex02_01
{
    using Ex02_Othelo;
    using static Ex02_Othelo.OtelloConstants;

    public class ComputerOpponent
    {
        private char computerSign;

        public char ComputerSign
        {
            get { return this.computerSign; }
            set { this.computerSign = value; }
        }

        // Random method
        /*public Move GetNextMove(OtehlloBoard i_gameBoard)
        {
            List<Move> computerMovesOptions = i_gameBoard.GetPossibleMoves(k_SecondPlayerSign);
            if (computerMovesOptions.Count == 0)
            {
                return null;
            }
            else
            {
                Random random = new Random();
                return computerMovesOptions[random.Next(computerMovesOptions.Count)];
            }
        }*/

        // AI method
        public Move GetNextMove(OtehlloBoard i_gameBoard)
        {
            MinimaxAI ai = new MinimaxAI(1, k_SecondPlayerSign);
            return ai.GetBestMove(i_gameBoard);
        }
    }
}
