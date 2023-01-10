namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using static Ex02_Othelo.OtelloConstants;

    public class OtehlloBoard
    {
        private readonly int[] r_RowDirectionOptions = { -1, -1, 0, 1, 1, 1, 0, -1 };
        private readonly int[] r_ColumnDirectionOptions = { 0, 1, 1, 1, 0, -1, -1, -1 };

        public OtehlloBoard(int io_Size)
        {
            this.BoardSize = io_Size;
            this.Rows = io_Size;
            this.Columns = io_Size;
            this.Grid = new char[this.Rows, this.Columns];
            this.initializeBoard();
        }

        public char[,] Grid { get; set; }

        public int BoardSize { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public static int ConvertLetterToNumber(char i_LetterToConvert)
        {
            int asciiValue = (int)i_LetterToConvert;

            // Check if char is a capital letter
            if (asciiValue >= 65 && asciiValue <= 90)
            {
                // Subtract 64 to convert capital letter to its corresponding number
                return asciiValue - 64;
            }

            // If char is not a capital letter, it must be a lowercase letter
            else
            {
                // Subtract 96 to convert lowercase letter to its corresponding number
                return asciiValue - 96;
            }
        }

        public static char convertNumberToCapitalLetter(int i_NumberToConvert)
        {
            return (char)('A' + i_NumberToConvert);
        }

        internal int GetSumOfdisks(char i_PlayerSign)
        {
            int totalOfPlayerDiskes = 0;

            for (int rowIterator = 0; rowIterator < this.Rows; rowIterator++)
            {
                for (int columnIterator = 0; columnIterator < this.Columns; columnIterator++)
                {
                    if (this.Grid[rowIterator, columnIterator] == i_PlayerSign)
                    {
                        totalOfPlayerDiskes += 1;
                    }
                }
            }

            return totalOfPlayerDiskes;
        }

        internal bool IsPlayerRunOutOfOptions(char i_PlayerSign)
        {
            bool v_PlayerOutOfOptions = true;
            if (this.GetPossibleMoves(i_PlayerSign).Count == 0)
            {
                return v_PlayerOutOfOptions;
            }

            return !v_PlayerOutOfOptions;
        }

        internal OtehlloBoard GetCopyBoard(Move i_move, char i_PlayerSign)
        {
            OtehlloBoard newBoard = new OtehlloBoard(this.BoardSize);

            for (int i = 0; i < this.BoardSize; i++)
            {
                for (int j = 0; j < this.BoardSize; j++)
                {
                    newBoard.Grid[i, j] = this.Grid[i, j];
                }
            }

            newBoard.makeMove(i_PlayerSign, i_move.RowIndex, i_move.ColumnIndex);
            return newBoard;
        }

        internal List<Move> GetPossibleMoves(char i_Player)
        {
            var possibleMoves = new List<Move>();

            // check each cell on the board
            for (int currentRow = 0; currentRow < this.Rows; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < this.Columns; currentColumn++)
                {
                    // if the cell is empty, check if it is a valid move
                    if (this.Grid[currentRow, currentColumn] == k_EmptyCellSign)
                    {
                        if (this.isValidMove(currentRow, currentColumn, i_Player))
                        {
                            possibleMoves.Add(new Move(currentRow, currentColumn));
                        }
                    }
                }
            }

            return possibleMoves;
        }

        internal void DisplayBoard()
        {
            printRowOfLetters(this.Rows);
            for (int rowsIterator = 0; rowsIterator < this.Rows; rowsIterator++)
            {
                printLineOfEqauelSigns(this.Rows);
                Console.Write(rowsIterator + 1 + k_Space + k_SeparatorSign); // Print the row number

                for (int columnsIterator = 0; columnsIterator < this.Columns; columnsIterator++)
                {
                    Console.Write(k_Space + this.Grid[rowsIterator, columnsIterator] + k_Space + k_SeparatorSign);
                }

                Console.WriteLine();
            }

            printLineOfEqauelSigns(this.Rows);
        }

        internal bool IsValidMove(int i_Row, char i_ColumnInLetterFormat, char i_CurrentPlayerSymbol)
        {
            return this.isValidMove(i_Row - 1, ConvertLetterToNumber(i_ColumnInLetterFormat) - 1, i_CurrentPlayerSymbol);
        }

        internal void MakeMove(char i_CurrentPlayerSymbol, int i_Row, char i_ColumnInLetterFormat)
        {
            this.makeMove(i_CurrentPlayerSymbol, i_Row - 1, ConvertLetterToNumber(i_ColumnInLetterFormat) - 1);
        }

        internal int Evaluate(char playerSign)
        {
            // Initialize the score to 0.
            int score = 0;

            // Loop through the board and count the number of discs for each player.
            int firstPlayerDiscs = this.GetSumOfdisks(k_FirstPlayerSign);
            int secondPlayerDiscs = this.GetSumOfdisks(k_SecondPlayerSign);

            // Calculate the score based on the number of discs for each player.
            if (playerSign == k_FirstPlayerSign)
            {
                score = firstPlayerDiscs - secondPlayerDiscs;
            }
            else
            {
                score = secondPlayerDiscs - firstPlayerDiscs;
            }

            return score;
        }

        internal bool IsGameOver()
        {
            return this.IsPlayerRunOutOfOptions(k_FirstPlayerSign) && this.IsPlayerRunOutOfOptions(k_SecondPlayerSign);
        }

        private static void printLineOfEqauelSigns(int i_NumOfColumns)
        {
            Console.Write(k_Space);
            int numOfEqulsSinsForEveryColumn = 3;
            for (int iterator = 0; iterator < i_NumOfColumns; iterator++)
            {
                Console.Write(k_EquelSign);
                for (int multiIterator = 0; multiIterator < numOfEqulsSinsForEveryColumn; multiIterator++)
                {
                    Console.Write(k_EquelSign);
                }
            }

            Console.WriteLine(k_EquelSign);
        }

        private static void printRowOfLetters(int i_NumOfColumns)
        {
            Console.Write(k_Space + k_Space);
            for (int iterator = 0; iterator < i_NumOfColumns; iterator++)
            {
                Console.Write(k_Space + k_Space + convertNumberToCapitalLetter(iterator) + k_Space);
            }

            Console.WriteLine();
        }

        private void initializeBoard()
        {
            for (int currentRowIndex = 0; currentRowIndex < this.Rows; currentRowIndex++)
            {
                for (int currentColumnIndex = 0; currentColumnIndex < this.Columns; currentColumnIndex++)
                {
                    this.Grid[currentRowIndex, currentColumnIndex] = k_EmptyCellSign;
                }
            }

            int midpoint = this.BoardSize / 2;
            this.Grid[midpoint - 1, midpoint - 1] = k_SecondPlayerSign;
            this.Grid[midpoint, midpoint] = k_SecondPlayerSign;
            this.Grid[midpoint - 1, midpoint] = k_FirstPlayerSign;
            this.Grid[midpoint, midpoint - 1] = k_FirstPlayerSign;
        }

        private bool isValidMove(int i_Row, int i_Column, char i_CurrentPlayerSymbol)
        {
            bool b_ValidMove = true;
            if (i_Row < 0 || i_Row >= this.Rows || i_Column < 0 || i_Column >= this.Columns)
            {
                return !b_ValidMove;
            }

            if (this.Grid[i_Row, i_Column] != k_EmptyCellSign)
            {
                return !b_ValidMove;
            }

            if (this.Grid[i_Row, i_Column] == i_CurrentPlayerSymbol)
            {
                return !b_ValidMove;
            }

            if (!this.IsCapturesOpponent(this, i_CurrentPlayerSymbol, i_Row, i_Column))
            {
                return !b_ValidMove;
            }

            return b_ValidMove;
        }

        private void makeMove(char currentPlayerSymbol, int row, int column)
        {
            // Check if the current cell is a valid move
            if (this.isValidMove(row, column, currentPlayerSymbol))
            {
                // Make the move and update the captured pieces
                this.UpdateCapturePieces(row, column, currentPlayerSymbol);
            }
            else
            {
                throw new ArgumentException("You Entered Invalid Move");
            }
        }

        private bool IsCapturesOpponent(OtehlloBoard i_Board, char i_CurrentPlayerSymbol, int i_Row, int i_Column)
        {
            bool capture = true;

            // Check for captures in all directions
            for (int i = 0; i < k_DirectionOptions; i++)
            {
                int currRow = i_Row + this.r_RowDirectionOptions[i];
                int currCol = i_Column + this.r_ColumnDirectionOptions[i];

                // Check if the current direction is valid
                if (this.isDiractionValid(currRow, currCol) && i_Board.Grid[currRow, currCol] != k_EmptyCellSign && i_Board.Grid[currRow, currCol] != i_CurrentPlayerSymbol)
                {
                    // Check for captures in the current direction
                    while (this.isDiractionValid(currRow, currCol) && i_Board.Grid[currRow, currCol] != k_EmptyCellSign && i_Board.Grid[currRow, currCol] != i_CurrentPlayerSymbol)
                    {
                        currRow += this.r_RowDirectionOptions[i];
                        currCol += this.r_ColumnDirectionOptions[i];
                    }

                    // Check if a capture was made in the current direction
                    if (this.isDiractionValid(currRow, currCol) && i_Board.Grid[currRow, currCol] == i_CurrentPlayerSymbol)
                    {
                        return capture;
                    }
                }
            }

            return !capture;
        }

        private bool isDiractionValid(int currRow, int currCol)
        {
            bool valid = true;
            if (currRow >= 0 && currRow < this.Rows && currCol >= 0 && currCol < this.Columns)
            {
                return valid;
            }

            return !valid;
        }

        private char getEnemyCharSign(char currentPlayerSymbol)
        {
            return (currentPlayerSymbol == k_FirstPlayerSign) ? k_SecondPlayerSign : k_FirstPlayerSign;
        }

        private void UpdateCapturePieces(int row, int column, char currentPlayerSymbol)
        {
            char enemy = this.getEnemyCharSign(currentPlayerSymbol);
            bool validMove = false;

            // Check for pieces to flip in each direction
            for (int neighborRowDistance = -1; neighborRowDistance <= 1; neighborRowDistance++)
            {
                for (int neighborColumnDistance = -1; neighborColumnDistance <= 1; neighborColumnDistance++)
                {
                    if (neighborRowDistance == 0 && neighborColumnDistance == 0)
                    {
                        continue;
                    }

                    int currentneighborRow = row + neighborRowDistance;
                    int currentneighborCulomn = column + neighborColumnDistance;
                    if (currentneighborRow < 0 || currentneighborRow >= this.Rows || currentneighborCulomn < 0 || currentneighborCulomn >= this.Columns || this.Grid[currentneighborRow, currentneighborCulomn] != enemy)
                    {
                        continue;
                    }

                    // Check for pieces to flip in this direction
                    while (currentneighborRow >= 0 && currentneighborRow < this.Rows && currentneighborCulomn >= 0 && currentneighborCulomn < this.Columns && this.Grid[currentneighborRow, currentneighborCulomn] == enemy)
                    {
                        currentneighborRow += neighborRowDistance;
                        currentneighborCulomn += neighborColumnDistance;
                    }

                    if (currentneighborRow >= 0 && currentneighborRow < this.Rows && currentneighborCulomn >= 0 && currentneighborCulomn < this.Columns && this.Grid[currentneighborRow, currentneighborCulomn] == currentPlayerSymbol)
                    {
                        validMove = true;
                        currentneighborRow -= neighborRowDistance;
                        currentneighborCulomn -= neighborColumnDistance;
                        while (currentneighborRow != row || currentneighborCulomn != column)
                        {
                            this.Grid[currentneighborRow, currentneighborCulomn] = currentPlayerSymbol;
                            currentneighborRow -= neighborRowDistance;
                            currentneighborCulomn -= neighborColumnDistance;
                        }
                    }
                }
            }

            if (validMove)
            {
                this.Grid[row, column] = currentPlayerSymbol;
            }
        }
    }
}
