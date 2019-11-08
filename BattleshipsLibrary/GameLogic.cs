using BattleshipsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipsLibrary
{
    public static class GameLogic
    {
        public static void InitialiseGrid(PlayerInfoModel model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }

            }
        }

        public static bool PlayerStillActive(PlayerInfoModel opponent)
        {
            // if there are no spots with a GridSpotStatus of Ship, then there are no ships left
            int shipsLeft = opponent.ShotGrid.Count(gridSpot => gridSpot.Status == GridSpotStatus.Ship);
            if(shipsLeft == 0)
            {
                return false;
            }
            return true;
            

        }

        private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(spot);
        }

        public static int GetShotCount(PlayerInfoModel winner)
        {
            int numHits = winner.ShotGrid.Count(gridSpot => gridSpot.Status == GridSpotStatus.Hit);
            int numMisses = winner.ShotGrid.Count(gridSpot => gridSpot.Status == GridSpotStatus.Miss);

            return numHits + numMisses;
        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            (string row, int column) = SplitShotIntoRowAndColumn(location);

            model.ShipLocations.Add(new GridSpotModel{ SpotLetter = row, SpotNumber = column});
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            string row = Convert.ToString(shot[0]);
            int column = Convert.ToInt32(shot[1]);

            return (row, column);
        }

        public static bool ValidateShot(PlayerInfoModel activePlayer, string row, int column)
        {
            
            bool rowOK = activePlayer.ShotGrid.Any(spot => spot.SpotLetter == row);
            bool colOK = activePlayer.ShotGrid.Any(spot => spot.SpotNumber == column);

            return rowOK && colOK;
            
            throw new NotImplementedException();
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            //find out if letter and number of shot are in shiplocations list.
            List<GridSpotModel> shipLocations = opponent.ShipLocations;
            foreach(GridSpotModel spot in shipLocations )
            {
                if(spot.SpotLetter == row && spot.SpotNumber == column)
                {
                    return true;
                }
                return false;
            }
            throw new NotImplementedException();
        }

        public static void MarkShotResult(PlayerInfoModel activePlayer, string row, int column, bool isAHit)
        {
            
        }
    }
}
