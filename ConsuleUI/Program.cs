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



            Console.ReadLine();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("Created by Jeremy Fox");
            Console.WriteLine();
        }

        private static PlayerInfoModel CreatePlayer()
        {
            PlayerInfoModel output = new PlayerInfoModel();

            //ask the user for their name
            output.UsersName = AskForUsersName();
            //Load up the shot grid
            GameLogic.InitialiseGrid(output);
            //Ask the user for their 5 ship placements
            //Clear the console
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

                bool isValidLocation = GameLogic.StoreShot(model, location);

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

        static void DisplayGrid()
        {

        }

        static void DisplayScore()
        {

        }

        static void ValidateShot() { 
}


    }
}
