using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class MapLocation
    {
        private string _name;
        private string _description;
        private LocationBounds _bounds;
    

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public LocationBounds Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }

        #region SRTUCTS
        public struct LocationBounds
        {
            public int nwRow, nwCol, seRow, seCol;

            public LocationBounds(int p1, int p2, int p3, int p4)
            {
                nwRow = p1;
                nwCol = p2;
                seRow = p3;
                seCol = p4;
            }
        }
        #endregion
    }
}
