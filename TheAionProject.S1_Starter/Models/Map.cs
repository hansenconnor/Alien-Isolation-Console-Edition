using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class Map
    {

        private string[,] _mapLayout;
        private string _mapString;

        public string MapString
        {
            get { return _mapString; }
            set { _mapString = value; }
        }

        public string[,] MapLayout
        {
            get { return _mapLayout; }
            set { _mapLayout = value; }
        }

        public string[,] drawMap()
        {

            //
            // TODO update array to hold map objects
            //

            int rows = 20;
            int columns = 50;

            string[,] mapLayout = new string[rows, columns];

            for (int i = 1; i != rows - 1; i++)
            {
                // not first or last row so set first and last col of each row to be a wall
                mapLayout[i, 0] = "#";
                mapLayout[i, columns - 1] = "#";
            }

            for (int i = 0; i != rows; i++)
            {
                if ((i == 0) || (i == rows - 1))
                {
                    // first or last row so only draw walls
                    for (int j = 0; j < columns; j++)
                    {
                        mapLayout[i, j] = "#";
                    }
                }
                else
                {
                    // not first or last row so draw cells between first and last col
                    for (int j = 1; j < columns - 1; j++)
                    {
                        mapLayout[i, j] = "-";
                    }
                }
            }

            //
            // add player to map
            mapLayout[2, 2] = "@";

            //
            // add NPC to map
            mapLayout[5, 5] = Universe.NPCs[0].Icon;

            //
            // add key object to map
            mapLayout[2, 5] = "K";

            return mapLayout;
        }

        // possibly update method to return pair of ints ( xCoor,yCoord ) instead of array of length 2
        public int[] getCurrentPosition(string[,] mapLayout)
        {
            int[] currentMapPosition = new int[2];

            for (int i = 0; i < mapLayout.GetLength(0); i++) // row
            {
                for (int j = 0; j < mapLayout.GetLength(1); j++) // col
                {
                    Debug.WriteLine(i + j);
                    if (mapLayout[i,j] == "@")
                    {
                        currentMapPosition[0] = i;
                        currentMapPosition[1] = j;
                    };
                }
            }
            // iterate through mapLaout and find player icon
            // return coordinates of player icon
            return currentMapPosition;
        }

        // take the current player position and redraw the map
        // update this method to take key as parameter ( up, down, left, right )
        // then determine new coords
        public string[,] updateMap( string[,] mapLayout, int[] currentPosition, ConsoleKey keyDirection )
        {
            switch (keyDirection)
            {
                // up arrow was pressed
                case ConsoleKey.UpArrow:
                    mapLayout = moveUp(mapLayout, currentPosition, keyDirection);
                    break;
                // down arrow was pressed
                case ConsoleKey.DownArrow:
                    mapLayout = moveDown(mapLayout, currentPosition, keyDirection);
                    break;
                // left arrow was pressed
                case ConsoleKey.LeftArrow:
                    mapLayout = moveLeft(mapLayout, currentPosition, keyDirection);
                    break;
                // right arrow was pressed
                case ConsoleKey.RightArrow:
                    mapLayout = moveRight(mapLayout, currentPosition, keyDirection);
                    break;
            }
            return mapLayout;
        }

        //
        // Player movement methods
        //
        public string[,] moveUp( string[,] mapLayout, int[] currentPosition, ConsoleKey keyDirection )
        {
            // variables to hold new row,col
            int newRow = 0;
            int newCol = 0;

            // variables to hold old row,col
            int oldRow = 0;
            int oldCol = 0;

            // update position
            newRow = currentPosition[0] - 1;
            newCol = currentPosition[1];

            oldRow = currentPosition[0]; // old row
            oldCol = currentPosition[1]; // old col

            // check cell type
            mapLayout = getCellType(mapLayout,currentPosition,keyDirection,newRow,newCol,oldRow,oldCol);

            return mapLayout;
        }

        public string[,] moveDown(string[,] mapLayout, int[] currentPosition, ConsoleKey keyDirection)
        {
            // variables to hold new row,col
            int newRow = 0;
            int newCol = 0;

            // variables to hold old row,col
            int oldRow = 0;
            int oldCol = 0;

            // update position
            newRow = currentPosition[0] + 1;
            newCol = currentPosition[1];

            oldRow = currentPosition[0]; // old row
            oldCol = currentPosition[1]; // old col

            // update the new cell
            mapLayout[newRow, newCol] = "@";

            // update the old cell
            mapLayout[oldRow, oldCol] = "-";
            //mapLayout[3nd row, 3rd column]

            return mapLayout;
        }
        public string[,] moveLeft(string[,] mapLayout, int[] currentPosition, ConsoleKey keyDirection)
        {
            // variables to hold new row,col
            int newRow = 0;
            int newCol = 0;

            // variables to hold old row,col
            int oldRow = 0;
            int oldCol = 0;

            // update position
            newRow = currentPosition[0];
            newCol = currentPosition[1] - 1;

            oldRow = currentPosition[0]; // old row
            oldCol = currentPosition[1]; // old col

            // update the new cell
            mapLayout[newRow, newCol] = "@";

            // update the old cell
            mapLayout[oldRow, oldCol] = "-";
            //mapLayout[3nd row, 3rd column]

            return mapLayout;
        }
        public string[,] moveRight(string[,] mapLayout, int[] currentPosition, ConsoleKey keyDirection)
        {
            // variables to hold new row,col
            int newRow = 0;
            int newCol = 0;

            // variables to hold old row,col
            int oldRow = 0;
            int oldCol = 0;

            // update position
            newRow = currentPosition[0];
            newCol = currentPosition[1] + 1;

            oldRow = currentPosition[0]; // old row
            oldCol = currentPosition[1]; // old col

            // update the new cell
            mapLayout[newRow, newCol] = "@";

            // update the old cell
            mapLayout[oldRow, oldCol] = "-";
            //mapLayout[3nd row, 3rd column]

            return mapLayout;
        }

        //
        // determine cell type and interactivity
        //
        // TODO call this method in the controller so I have access to _gameConsoleView Header text 
        public string[,] getCellType(string[,] mapLayout, int[] currentPosition, ConsoleKey keyDirection, int newRow, int newCol, int oldRow, int oldCol)
        {
            // check if wall
            if (mapLayout[newRow, newCol] == "#")
            {
                Console.WriteLine("That's a wall! You can't go that way!");
            }
            // check if empty cell
            else if (mapLayout[newRow, newCol] == "-")
            {
                // update the new cell
                mapLayout[newRow, newCol] = "@";

                // update the old cell
                mapLayout[oldRow, oldCol] = "-";
            }
            // check if NPC
            else
            {
                foreach (NPC npc in Universe.NPCs)
                {
                    if (npc.Icon == mapLayout[newRow, newCol])
                    {
                        // display NPC menu
                        Console.WriteLine(npc.Name);
                        // TODO: need to return the npc or dialogue to call _gameConsoleView display Message box
                        Console.ReadLine();
                        break;
                    }
                }
            }
            return mapLayout;
        }

        public int[] getTile( string[,] mapLayout, int[] currentPosition, ConsoleKey keyDirection )
        {
            int[] currentMapPosition = new int[2];

            int newRow = 0;
            int newCol = 0;

            switch (keyDirection)
            {
                case ConsoleKey.UpArrow:
                    newRow = currentPosition[0] - 1;
                    newCol = currentPosition[1];
                    break;
                case ConsoleKey.DownArrow:
                    newRow = currentPosition[0] + 1;
                    newCol = currentPosition[1];
                    break;
                case ConsoleKey.LeftArrow:
                    newRow = currentPosition[0];
                    newCol = currentPosition[1] - 1;
                    break;
                case ConsoleKey.RightArrow:
                    newRow = currentPosition[0];
                    newCol = currentPosition[1] + 1;
                    break;
                default:
                    break;
            }

            currentMapPosition[0] = newRow;
            currentMapPosition[1] = newCol;

            return currentMapPosition;
        }

        public bool validateCellType( string[,] mapLayout, int[] currentPosition, ConsoleKey keyDirection )
        {
            // variables to hold new row,col
            int newRow = 0;
            int newCol = 0;

            // variables to hold old row,col
            int oldRow = 0;
            int oldCol = 0;

            switch (keyDirection)
            {
                case ConsoleKey.UpArrow:
                    newRow = currentPosition[0] - 1;
                    newCol = currentPosition[1];
                    break;
                case ConsoleKey.DownArrow:
                    newRow = currentPosition[0] + 1;
                    newCol = currentPosition[1];
                    break;
                case ConsoleKey.LeftArrow:
                    newRow = currentPosition[0];
                    newCol = currentPosition[1] - 1;
                    break;
                case ConsoleKey.RightArrow:
                    newRow = currentPosition[0];
                    newCol = currentPosition[1] + 1;
                    break;
                default:
                    break;
            }

            // check if wall
            if (mapLayout[newRow, newCol] == "#")
            {
                // is wall so return false
                return false;
            }
            else
            {
                // not a wall so return true
                return true;
            }
        }


        // take the 2D map array and convert to a string
        public string convertMapToString(string[,] mapLayout)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            for (int i = 0; i < mapLayout.GetLength(0); i++)
            {
                for (int j = 0; j < mapLayout.GetLength(1); j++)
                {
                    sb.Append(mapLayout[i, j]);
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        // method to process player keystrokes
        public bool validateKeyStroke(ConsoleKey keyInfo)
        {
            // TODO check if player is in menu 
            // TODO check a class list of valid keys
            if ((keyInfo == ConsoleKey.UpArrow) || (keyInfo == ConsoleKey.DownArrow) || (keyInfo == ConsoleKey.LeftArrow) || (keyInfo == ConsoleKey.RightArrow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
