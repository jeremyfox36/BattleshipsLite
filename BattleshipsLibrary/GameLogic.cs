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

        public static bool PlayerStillActive(PlayerInfoModel player)
        {
            bool isActive = false;
            // if there are no spots with a GridSpotStatus of Ship, then there are no ships left
            foreach (var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    isActive = true;
                }
            }
            return isActive;
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

        public static int GetShotCount(PlayerInfoModel player)
        {
            int shotCount = 0;
            foreach (var shot in player.ShotGrid)
            {
                if (shot.Status != GridSpotStatus.Empty)
                {
                    shotCount += 1;
                }
            }
            return shotCount;
        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            bool output = false;
            (string row, int column) = SplitShotIntoRowAndColumn(location);

            bool isValidLocation = ValidateGridLocation(model, row, column);
            bool isSpotOpen = ValidateShipLocation(model, row, column);


            if (isValidLocation == true && isSpotOpen == true)
            {
                model.ShipLocations.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });
            }

            output = true;
            return output;
        }

        private static bool ValidateShipLocation(PlayerInfoModel model, string row, int column)
        {
            bool isValidLocation = true;

            foreach (var ship in model.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = false;
                }
            }

            return isValidLocation;
        }

        private static bool ValidateGridLocation(PlayerInfoModel model, string row, int column)
        {
            bool isValidLocation = false;

            foreach (var ship in model.ShotGrid)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = true;
                }
            }

            return isValidLocation;

        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            if (shot.Length != 2)
            {
                throw new ArgumentException("This was an invalid shot type", "shot");
            }

            char[] shotArray = shot.ToArray();

            string row = "";
            int column = 0;

            row = Convert.ToString(shotArray[0]);
            column = Convert.ToInt32(shotArray[1].ToString());

            return (row, column);
        }

        public static bool ValidateShot(PlayerInfoModel player, string row, int column)
        {
            bool isValidShot = false;

            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (gridSpot.Status == GridSpotStatus.Empty)
                    {
                        isValidShot = true;
                    }
                }
            }

            return isValidShot;

            //bool rowOK = player.ShotGrid.Any(spot => spot.SpotLetter == row);
            //bool colOK = player.ShotGrid.Any(spot => spot.SpotNumber == column);

            //return rowOK && colOK;

            //throw new NotImplementedException();
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            bool isAHit = false;

            foreach (var ship in opponent.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isAHit = true;
                }
            }

            return isAHit;
        }

        public static void MarkShotResult(PlayerInfoModel activePlayer, PlayerInfoModel opponent, string row, int column, bool isAHit)
        {
            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        gridSpot.Status = GridSpotStatus.Hit;
                        foreach(var ship in opponent.ShipLocations)
                        {
                            if(ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                            {
                                ship.Status = GridSpotStatus.Sunk;
                            }
                        }
                    }
                    else
                    {
                        gridSpot.Status = GridSpotStatus.Miss;
                    }
                }
            }
        }   
    }
}
