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

            string[,] mapLayout = new string[6, 10]
            {
                {"#","#","#","#","#","#","#","#","#","#"},
                {"#","-","-","-","-","-","-","-","-","#"},
                {"#","-","-","-","-","-","-","-","-","#"},
                {"#","-","-","-","-","-","-","-","-","#"},
                {"#","-","-","-","-","-","-","-","-","#"},
                {"#","#","#","#","#","#","#","#","#","#"}
            };

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

                    // update the new cell
                    mapLayout[newRow, newCol] = "@";

                    // update the old cell
                    mapLayout[oldRow, oldCol] = "-";
                    //mapLayout[3nd row, 3rd column]
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
