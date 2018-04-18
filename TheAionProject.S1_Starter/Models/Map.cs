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

            int rows = 20;
            int columns = 115;

            string[,] mapLayout = new string[rows, columns];

            for (int i = 1; i != rows - 1; i++)
            {
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
                    // not first or last row so draw cells
                    for (int j = 1; j < columns - 1; j++)
                    {
                        mapLayout[i, j] = "-";
                    }
                }
            }


            mapLayout[2, 2] = "@";

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
            // variables to hold new row,col
            int newRow = 0;
            int newCol = 0;

            // variables to hold old row,col
            int oldRow = 0;
            int oldCol = 0;

            switch (keyDirection)
            {
                // up arrow was pressed
                case ConsoleKey.UpArrow:
                    newRow = currentPosition[0] - 1;
                    newCol = currentPosition[1];

                    oldRow = currentPosition[0]; // old row
                    oldCol = currentPosition[1]; // old col

                    //
                    // check the requested cell for map object type
                    //
                    if (mapLayout[newRow,newCol] == "#")
                    {
                        Console.WriteLine("That's a wall! You can't go that way!");
                    }
                    else if (mapLayout[newRow, newCol] == "T")
                    {
                        Console.WriteLine("A troll appears!");
                        // do some action
                    }
                    else // update player position
                    {
                        // update the new cell
                        mapLayout[newRow, newCol] = "@";

                        // update the old cell
                        mapLayout[oldRow, oldCol] = "-";
                    }




                    break;

                // down arrow was pressed
                case ConsoleKey.DownArrow:
                    newRow = currentPosition[0] + 1;
                    newCol = currentPosition[1]; 
                    
                    oldRow = currentPosition[0]; // old row
                    oldCol = currentPosition[1]; // old col

                    // update the new cell
                    mapLayout[newRow, newCol] = "@";

                    // update the old cell
                    mapLayout[oldRow, oldCol] = "-";
                    //mapLayout[3nd row, 3rd column]
                    break;
                // left arrow was pressed
                case ConsoleKey.LeftArrow:
                    newRow = currentPosition[0];
                    newCol = currentPosition[1] - 1;

                    oldRow = currentPosition[0]; // old row
                    oldCol = currentPosition[1]; // old col

                    // update the new cell
                    mapLayout[newRow, newCol] = "@";

                    // update the old cell
                    mapLayout[oldRow, oldCol] = "-";
                    //mapLayout[3nd row, 3rd column]
                    break;
                // right arrow was pressed
                case ConsoleKey.RightArrow:
                    newRow = currentPosition[0];
                    newCol = currentPosition[1] + 1;

                    oldRow = currentPosition[0]; // old row
                    oldCol = currentPosition[1]; // old col

                    // update the new cell
                    mapLayout[newRow, newCol] = "@";

                    // update the old cell
                    mapLayout[oldRow, oldCol] = "-";
                    //mapLayout[3nd row, 3rd column]
                    break;
            }
            return mapLayout;
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


    }
}
