using System;

namespace Tic_Tac_Toe {
    class Program {
        static void Main(string[] args){
            bool donePlaying = false;

            while (!donePlaying) {
                PlayGame();

                Console.WriteLine("Play again? Y or N");
                char playAgain = Console.ReadLine()[0];
                if(Char.ToUpper(playAgain) != 'Y' && Char.ToUpper(playAgain) != 'N')
                    while (!ValidateInput(ref playAgain)) { }

                if (Char.ToUpper(playAgain) == 'N')
                    break;

                Console.Clear();
            }
        }

        // Play a game
        static void PlayGame() {
            // Game board
            string[,] board = new string[3, 3]{
                                                {"7", "8", "9" },
                                                {"4", "5", "6" },
                                                {"1", "2", "3" }
                                              };

            // Flag for tie game
            // Starts true, gets set to false if win is found
            bool gameIsATie = true;

            // Game ends after all spots on the board are filled
            int currentTurn = 0;

            string currentPlayer = "X";
            while (currentTurn < 9) {
                PrintBoard(board);

                int spot = ChooseSpot(currentPlayer, board);

                UpdateBoard(board, spot, currentPlayer);

                if (CheckForWin(board)) {
                    gameIsATie = false;
                    break;
                }

                currentTurn++;

                // Swap players for next turn
                currentPlayer = currentPlayer == "X" ? "O" : "X";
            }

            PrintBoard(board);

            // If tie flag is still true, game is a tie
            // Otherwise the last player to choose a spot wins
            string winMsg;
            if (gameIsATie)
                winMsg = "Tie game";
            else
                winMsg = currentPlayer + " Wins!";

            Console.WriteLine("---------\n" + winMsg);
        }

        // Print the board to the console
        static void PrintBoard(string[,] board) {
            Console.WriteLine("-------");
            for(int i = 0; i < 3; i++) {
                Console.WriteLine("|" + board[i, 0] + "|" + board[i, 1] + "|" + board[i, 2] + "|");
            }
            Console.WriteLine("-------");
        }

        // Takes in player chosen spot and updates the game board
        static void UpdateBoard(string[,] board, int spot, string player) {
            int column = 0;
            int row = 0;

            FindSpotOnBoard(spot, ref column, ref row);

            board[row, column] = player;
        }

        // Given a spot number, will find column and row numbers for the 2D array
        static void FindSpotOnBoard(int spot, ref int column, ref int row) {
            // If spot is > 6 : Top row
            // If spot is > 3 < 6 : Middle row
            // If spot is < 3 : Bottom row
            row = 0;
            for(int i = 6; i >= 0; i-= 3) {
                if(spot > i) {
                    column = spot - i - 1;
                    break;
                }

                row++;
            }
        }

        // Current player chooses a spot on the game board
        static int ChooseSpot(string player, string[,] board) {
            const int MAX_SPOT = 9;
            const int MIN_SPOT = 1;

            Console.WriteLine(player + " choose a spot on the board");

            int chosenSpot;

            bool validInput = int.TryParse(Console.ReadLine(), out chosenSpot);
            while(!validInput || chosenSpot < MIN_SPOT || chosenSpot > MAX_SPOT || !ValidateSpot(chosenSpot, board)) {
                string errMsg;
                if (validInput)
                    errMsg = "Chosen spot is either already taken or not on the board";
                else
                    errMsg = "Please choose a number from 1 to 9";

                validInput = ValidateInput(ref chosenSpot, MIN_SPOT, MAX_SPOT, errMsg);
            }

            return chosenSpot;
        }

        // Checks if a win condition has been met
        static bool CheckForWin(string[,] board) {
            // Win condition: All spots in a row
            for (int i = 0; i < 3; i++) {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
            }

            // Win condition: All spots in a column
            for (int i = 0; i < 3; i++) {
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }

            // Win condition: Diagonal
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                // x--
                // -X-
                // --x
                return true;

            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                // --X
                // -X-
                // x--
                return true;

            return false;
        }

        // Checks if spot on the board has already been taken
        static bool ValidateSpot(int spot, string[,] board) {
            int column = 0;
            int row = 0;

            FindSpotOnBoard(spot, ref column, ref row);

            return board[row, column] != "X" && board[row, column] != "O";
        }

        // Validates int input
        // Used for choosing spot on the board
        static bool ValidateInput(ref int value, int min, int max, string errMsg) {
            Console.WriteLine(errMsg);
            
            bool validInput = int.TryParse(Console.ReadLine(), out value);
            
            return validInput;
        }

        // Validates char input
        // Used for selecting if user wants to play again
        static bool ValidateInput(ref char value) {
            Console.WriteLine("Play Again? \nPlease enter [Y]es or [N]o");
            value = Console.ReadLine()[0];

            return Char.ToUpper(value) == 'Y' || Char.ToUpper(value) == 'N';
        }
    }
}
