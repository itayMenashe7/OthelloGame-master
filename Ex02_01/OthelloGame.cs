// Black sign belong to the first player
// White sign belnog to the second player(computer/human).
namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Ex02.ConsoleUtils;
    using Ex02_01;
    using static Ex02_Othelo.OtelloConstants;

    public enum eGameModes
    {
        ComputerMode = 1,
        TwoPlayersMode = 2,
    }

    public enum eBoardSizes
    {
        SixOnSix = 6,
        EightOnEight = 8,
    }

    public enum eCurrentPlayers
    {
        FirstHumenPlayer,
        SecondHumanPlayer,
        ComputerOpponent,
    }

    internal class OthelloGame
    {
        private HumanPlayer firstHumanPlayer = new HumanPlayer();
        private HumanPlayer secondHumanPlayer = null;
        private ComputerOpponent computerOpponent = null;
        private eGameModes gameMode;
        private OtehlloBoard gameBoard = null;
        private StringBuilder print = new StringBuilder();
        private eCurrentPlayers currentPlayer = eCurrentPlayers.FirstHumenPlayer;

        public static void WelcomeGameMessage()
        {
            Console.WriteLine(k_WelcomeMessage);
        }

        public string GetStringInputFromUser(string i_message)
        {
            Console.WriteLine(i_message);
            string input = Console.ReadLine();
            this.QuitFromGame(input);
            return input;
        }

        public void PrintInvlidInputMessgae(string message)
        {
            Screen.Clear();
            this.gameBoard.DisplayBoard();
            Console.WriteLine(message);
        }

        public eGameModes ChooseGameMode()
        {
            while (true)
            {
                string userInput = this.GetStringInputFromUser(k_ChooseGameModeMessage);
                if (int.TryParse(userInput, out int userModeChoose))
                {
                    if (userModeChoose == ((int)eGameModes.ComputerMode))
                    {
                        return eGameModes.ComputerMode;
                    }
                    else if (userModeChoose == ((int)eGameModes.TwoPlayersMode))
                    {
                        return eGameModes.TwoPlayersMode;
                    }
                    else
                    {
                        Console.WriteLine(k_InvalidValueMessage);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine(k_InvalidValueMessage);
                }
            }
        }

        public void QuitFromGame(string io_userInputValue)
        {
            if (io_userInputValue.ToLower() == k_quitChar)
            {
                this.StopApp();
            }
        }

        public void StopApp()
        {
            Console.WriteLine(k_StopGameMessgae);
            Thread.Sleep(2000);
            Environment.Exit(0);
        }

        internal string GetCurrentPlayerName(eCurrentPlayers i_currentPlayer)
        {
            string returnValue;
            switch (i_currentPlayer)
            {
                case eCurrentPlayers.ComputerOpponent:
                    returnValue = "Boti";
                    break;
                case eCurrentPlayers.FirstHumenPlayer:
                    returnValue = this.firstHumanPlayer.Name;
                    break;
                case eCurrentPlayers.SecondHumanPlayer:
                    returnValue = this.secondHumanPlayer.Name;
                    break;
                default:
                    returnValue = "Unknown";
                    break;
            }

            return returnValue;
        }

        internal string GetPlayerNameBySign(char i_playerSign)
        {
            if (i_playerSign == k_FirstPlayerSign)
            {
                return this.firstHumanPlayer.Name;
            }
            else
            {
                if (this.gameMode == eGameModes.ComputerMode)
                {
                    return "Boti";
                }
                else
                {
                    return this.secondHumanPlayer.Name;
                }
            }
        }

        internal void Run(bool i_IsNewGameFlag)
        {
            bool v_IsGameNotOver = true;
            int v_EndGameFlag = 0;
            if (!i_IsNewGameFlag)
            {
                this.initGame();
            }

            while (v_IsGameNotOver)
            {
                Screen.Clear();
                if (v_EndGameFlag == k_EndGameFlag)
                {
                    v_IsGameNotOver = false;
                    continue;
                }

                this.gameBoard.DisplayBoard();
                if (this.gameBoard.IsPlayerRunOutOfOptions(this.getPlayerSign(this.currentPlayer)))
                {
                    v_EndGameFlag++;
                    this.printOutOfOptionsMessage();
                    this.updateNextPlayer();
                    continue;
                }

                if (this.playerNextMove())
                {
                    this.updateNextPlayer();
                }
            }

            this.gameOver();
            this.wantToPlayAgain();
        }

        private bool playerNextMove()
        {
            bool v_IsPlayerPlayInvalidMove = true;
            string nextMoveinput;
            while (v_IsPlayerPlayInvalidMove)
            {
                if (this.currentPlayer == eCurrentPlayers.ComputerOpponent)
                {
                    Move comuterNextMove = this.computerOpponent.GetNextMove(this.gameBoard);
                    this.gameBoard.MakeMove(this.getPlayerSign(this.currentPlayer), comuterNextMove.RowIndex + 1, OtehlloBoard.convertNumberToCapitalLetter(comuterNextMove.ColumnIndex));
                    return v_IsPlayerPlayInvalidMove;
                }
                else
                {
                    nextMoveinput = this.GetStringInputFromUser(k_EnterNextMoveMessage);

                    if (this.IsUserInputInValidFormat(nextMoveinput))
                    {
                        if (this.gameBoard.IsValidMove(nextMoveinput[1] - k_ValueForConvertingAsciNumberToIntNumber, nextMoveinput[0], this.getPlayerSign(this.currentPlayer)))
                        {
                            this.gameBoard.MakeMove(this.getPlayerSign(this.currentPlayer), nextMoveinput[1] - k_ValueForConvertingAsciNumberToIntNumber, nextMoveinput[0]);
                            return v_IsPlayerPlayInvalidMove;
                        }
                        else
                        {
                            this.PrintInvlidInputMessgae(k_InvalidCellMessage);
                            continue;
                        }
                    }
                    else
                    {
                        this.PrintInvlidInputMessgae(k_InvalidMoveFormatMessage);
                        continue;
                    }
                }
            }

            return !v_IsPlayerPlayInvalidMove;
        }

        private bool IsUserInputInValidFormat(string i_input)
        {
            bool v_IsInputIncorrectFormat = true;

            if (i_input.Length < 2 || i_input.Length > 2)
            {
                return !v_IsInputIncorrectFormat;
            }

            if (!Regex.IsMatch(i_input[1] + string.Empty, @"^\d+$"))
            {
                return !v_IsInputIncorrectFormat;
            }

            if (!Regex.IsMatch(i_input[0] + string.Empty, "^[a-zA-Z0-9]*$"))
            {
                return !v_IsInputIncorrectFormat;
            }

            return v_IsInputIncorrectFormat;
        }

        private char getPlayerSign(eCurrentPlayers i_player)
        {
            if (i_player == eCurrentPlayers.FirstHumenPlayer)
            {
                return k_FirstPlayerSign;
            }

            return k_SecondPlayerSign;
        }

        private void updateNextPlayer()
        {
            if (this.currentPlayer == eCurrentPlayers.FirstHumenPlayer)
            {
                this.currentPlayer = (this.gameMode == eGameModes.ComputerMode) ? eCurrentPlayers.ComputerOpponent : eCurrentPlayers.SecondHumanPlayer;
            }
            else if (this.currentPlayer == eCurrentPlayers.SecondHumanPlayer)
            {
                this.currentPlayer = (this.gameMode == eGameModes.ComputerMode) ? eCurrentPlayers.ComputerOpponent : eCurrentPlayers.FirstHumenPlayer;
            }
            else
            {
                this.currentPlayer = eCurrentPlayers.FirstHumenPlayer;
            }
        }

        private void gameOver()
        {
            this.print.Clear();
            int firstPlayerScore = this.gameBoard.GetSumOfdisks(k_FirstPlayerSign);
            int secondPlayerScore = this.gameBoard.GetSumOfdisks(k_SecondPlayerSign);
            this.print.AppendFormat(k_EndGameResulatMessage, this.GetPlayerNameBySign(k_FirstPlayerSign), firstPlayerScore, this.GetPlayerNameBySign(k_SecondPlayerSign), secondPlayerScore).AppendLine();
            if (firstPlayerScore < secondPlayerScore)
            {
                this.print.AppendFormat(k_WinnerAnnouncementMessage, this.GetPlayerNameBySign(k_SecondPlayerSign));
            }
            else if (firstPlayerScore > secondPlayerScore)
            {
                this.print.AppendFormat(k_WinnerAnnouncementMessage, this.GetPlayerNameBySign(k_FirstPlayerSign));
            }
            else
            {
                this.print.AppendLine(k_EndGameDrawResulatMessage);
            }

            Console.WriteLine(this.print);
            Thread.Sleep(5000);
        }

        private void wantToPlayAgain()
        {
            string userInput = this.GetStringInputFromUser(k_WantToPlayAgainMessage);
            Console.WriteLine(k_WantToPlayAnotherGameMessage);
            this.QuitFromGame(userInput);
            this.gameBoard = new OtehlloBoard(this.gameBoard.BoardSize);
            bool newGameFlag = true;
            this.Run(newGameFlag);
        }

        private void printOutOfOptionsMessage()
        {
            Console.WriteLine(k_PassTurnMessage, this.GetCurrentPlayerName(this.currentPlayer));
            Thread.Sleep(3000);
        }

        private void initGame()
        {
            WelcomeGameMessage();
            this.firstHumanPlayer.Name = this.GetStringInputFromUser(k_EnterFirstPlayerNameMessage);
            this.firstHumanPlayer.PlayerSign = k_FirstPlayerSign;
            this.gameMode = this.ChooseGameMode();
            if (this.gameMode == eGameModes.TwoPlayersMode)
            {
                this.secondHumanPlayer = new HumanPlayer();
                this.secondHumanPlayer.Name = this.GetStringInputFromUser(k_EnterSecondHumanPlayerName);
                this.secondHumanPlayer.PlayerSign = k_SecondPlayerSign;
            }
            else
            {
                this.computerOpponent = new ComputerOpponent();
                this.computerOpponent.ComputerSign = k_SecondPlayerSign;
            }

            this.gameBoard = new OtehlloBoard((int)this.chooseSizeOfBorad());
        }

        private eBoardSizes chooseSizeOfBorad()
        {
            while (true)
            {
                string userInput = this.GetStringInputFromUser(k_ChooseBoardSizeMessage);
                if (int.TryParse(userInput, out int boardSizeChoose))
                {
                    if (boardSizeChoose == ((int)eBoardSizes.SixOnSix))
                    {
                        return eBoardSizes.SixOnSix;
                    }
                    else if (boardSizeChoose == ((int)eBoardSizes.EightOnEight))
                    {
                        return eBoardSizes.EightOnEight;
                    }
                    else
                    {
                        Console.WriteLine(k_InvalidValueMessage);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine(k_InvalidValueMessage);
                }
            }
        }
    }
}