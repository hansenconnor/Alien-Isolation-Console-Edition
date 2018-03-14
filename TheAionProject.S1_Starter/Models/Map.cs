using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class Map
    {
        // add propf for mapLayout

        public int[] drawMap()
        {

            string[,] mapLayout = new string[6, 5]
            {
                {"#","#","#","#","#"},
                {"#","-","-","-","#",},
                {"#","-","-","-","#",},
                {"#","-","-","-","#",},
                {"#","-","-","-","#",},
                {"#","#","#","#","#"}
            };

            int[] initialPosition = new int[] { 2, 2 };

            int rowLength = mapLayout.GetLength(0);
            int colLength = mapLayout.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", mapLayout[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ReadLine();
            return initialPosition;
        }

        public int[] getCurrentPosition()
        {
            int[] currentMapPosition = new int[] { 2, 2 };
            // iterate through mapLaout and find player icon
            // return coordinates of player icon
            return currentMapPosition;
        }


    }
}
