using System;
using BattleshipsLibrary;
using BattleshipsLibrary.Models;

namespace BattleshipLite
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");

            PlayerInfoModel winner = null;

            do
            {
                // Display grid from activePlayer on where they fired
                DisplayShotGrid(activePlayer);
                // ask activePlayer for a shot
                // determine if it is a valid shot
                // determine shot results
                RecordPlayerShot(activePlayer, opponent);
                // determine if the game is over
                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);
                // if over set activePlayer as the winner
                if(doesGameContinue == true)
                {
                    // swap the players using a Tuple
                    
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                }
                // else, swap positions (activePlayer to opponent)

            } while (winner == null);

            Console.ReadLine();
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            // asks for a shot (we ask for "B2")
            // determine what row and column that is
            // determine if valid shot
            // go back to the beginning if not a valid shot
            
            // determine the result of the shot - hit or miss
            // record results
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if(gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }
                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" { gridSpot.SpotLetter}{gridSpot.SpotNumber}");
                }
                else if(gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" x ");
                }
                else if(gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                {
                    Console.Write(" ? ");
                }

            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("Created by Jeremy Fox");
            Console.WriteLine();
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();
            Console.WriteLine($"Player information for { playerTitle }");
            //ask the user for their name
            output.UsersName = AskForUsersName();
            //Load up the shot grid
            GameLogic.InitialiseGrid(output);
            //Ask the user for their 5 ship placements
            PlaceShips(output);
            //Clear the console
            Console.Clear();

            return output;
        }

        private static string AskForUsersName()
        {
            Console.WriteLine("What is your name: ");

            string output = Console.ReadLine();

            return output;
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where do you want to place ship number { model.ShipLocations.Count + 1 }: ");
                string location = Console.ReadLine();

                bool isValidLocation = GameLogic.PlaceShip(model, location);

                if (isValidLocation == false)
                {
                    Console.WriteLine("That was not a valid location. Please try again.");
                }

            } while (model.ShipLocations.Count < 5);
        }

        static void GetUsersShipPlacements()
        {

        }

        static void ValidateShipPlacement()
        {

        }

        static void StoreShot()
        {

        }

        static void CreateUserGrid()
        {

        }

        static void FireShot()
        {

        }

        static void DisplayScore()
        {

        }

        static void ValidateShot()
        {
        }


    }
}
