

namespace Ex02_Othelo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class OtelloConstants
    {
        public const char k_FirstPlayerSign = 'X';
        public const char k_SecondPlayerSign = 'O';
        public const char k_EmptyCellSign = ' ';
        public const string k_Space = " ";
        public const char k_SeparatorSign = '|';
        public const char k_EquelSign = '=';

        public const string k_WelcomeMessage = "Welcome to the Othello game!!";
        public const string k_ChooseGameModeMessage = "Choose game mode:\n" +
            "Play against the computer - pressing 1.\n" +
            "play against human - press 2.";

        public const string k_InvalidValueMessage = "The option you selected is invalid, please try again";
        public const string k_AfterChoosingGameModeMessage = "The option you selected is invalid, please try again";
        public const string k_EnterFirstPlayerNameMessage = "Enter youe  name";
        public const string k_EnterSecondHumanPlayerName = "Enter second player name";
        public const string k_ChooseBoardSizeMessage = "Soon we will start, but before that enter board size you want to play.\nFor 8X8 press 8\nFor 6X6 press 6";
        public const string k_PassTurnMessage = "There is no move that can be made by {0}, so the turn passes to the next player";

        public const string k_EnterNextMoveMessage = "Please enter your next move";
        public const string k_StopGameMessgae = "You chose to quit the game, thanks for playing, goodbye :)";
        public const string k_InvalidMoveFormatMessage = "The format you entered is incorrect, please make sure there are only 2 characters in youe input.\n" +
            "The first - A letter that represents the column.\n" +
            "The second - A number that represents the row";

        public const string k_InvalidCellMessage = "The Move you entered is not possible, please try again according to the appropriate game rules";
        public const string k_WinnerAnnouncementMessage = "\n{0} wins the game!!!";
        public const string k_EndGameResulatMessage = "game over!\nThe score is:\n{0} - {1}.\n{2} - {3}";
        public const string k_EndGameDrawResulatMessage = "game over!\nThe score is:\n{0} - {1}.\n{2} - {3}";

        public const string k_WantToPlayAgainMessage = "\nDo you want to play again?\nNo - insert the Q character\nYes - type something";
        public const string k_WantToPlayAnotherGameMessage = "You chose to start a new game :)\nLet's Play!";

        public const string k_quitChar = "q";
        public const int k_EndGameFlag = 2;
        public const int k_DirectionOptions = 8;
        public const char k_ValueForConvertingAsciNumberToIntNumber = '0';
    }
}
