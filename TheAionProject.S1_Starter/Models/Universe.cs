using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class Universe
    {
        //#region PROPERTIES
        //private List<MapObject> _mapObjects;
        //#endregion


        //#region FIELDS
        //private List<MapObject> MapObjects
        //{
        //    get { return _mapObjects; }
        //    set { _mapObjects = value; }
        //}
        //#endregion

        //
        // list of map objects: doors, keys, chests, etc.
        public static List<MapObject> mapObjects = new List<MapObject>()
        {
            new MapObject()
            {
                Name = "Master Key",
                Icon = "K",
                Interactable = true
            }
        };

        //
        // list of NPCs
        public static List<NPC> NPCs = new List<NPC>()
        {
            new NPC()
            {
                Name = "Test Npc",
                Icon = "P",
                Interactable = true
            }
        };

    }
}
