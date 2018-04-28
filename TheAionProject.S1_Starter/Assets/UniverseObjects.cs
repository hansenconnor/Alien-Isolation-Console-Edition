using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public static partial class UniverseObjects
    {
        public static List<MapObject> mapObjects = new List<MapObject>()
        {
            new MapObject
            {
                Name = "The First Door",
                Coords = new int [2] { 3, 10 },
                Type = MapObjectType.Door
            },
        };

        public static List<MapLocation> mapLocations = new List<MapLocation>()
        {
            new MapLocation
            {
                ID = 1,
                Name = "The First Room",
                Bounds = new MapLocation.LocationBounds(1,1,6,15)
            },
            new MapLocation
            {
                ID = 2,
                Name = "The Second Room",
                Bounds = new MapLocation.LocationBounds(1,16,6,32)
            }
            // TODO add 4 more map locations
        };

        public static List<GameObject> gameObjects = new List<GameObject>()
        {
            new TravelerObject
            {
                Id = 1,
                Icon = "G",
                Name = "Bag of Gold",
                SpaceTimeLocationId = 2,
                Description = "A small leather pouch filled with 9 gold coins.",
                Type = TravelerObjectType.Treasure,
                Value = 45,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new TravelerObject
            {
                Id = 2,
                Name = "Ruby of Saron",
                SpaceTimeLocationId = 3,
                Description = "A bright red jewel, roughly the size of a robin's egg.",
                Type = TravelerObjectType.Treasure,
                Value = 45,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new TravelerObject
            {
                Id = 3,
                Name = "Rotogenic Medicine",
                SpaceTimeLocationId = 3,
                Description = "A wooden box containing a small vial filled with a blue liquid.",
                Type = TravelerObjectType.Medicine,
                Value = 45,
                CanInventory = false,
                IsConsumable = true,
                IsVisible = true
            },

            new TravelerObject
            {
                Id = 4,
                Name = "Norlan Document ND-3075",
                SpaceTimeLocationId = 3,
                Description =
                    "Memo: Origin Errata" + "/n" +
                    "Universal Date: 378598" + "/n" +
                    "/n" +
                    "It appears a potential origin for the technology is based on Plenatia 5 in the Star Reach Galaxy.",
                Type = TravelerObjectType.Information,
                Value = 0,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new TravelerObject
            {
                Id = 8,
                Name = "Aion Tracker",
                SpaceTimeLocationId = 0,
                Description =
                    "Standard issue device worn around wrist that allows for tracking and messaging.",
                Type = TravelerObjectType.Information,
                Value = 0,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },

            new TravelerObject
            {
                Id = 9,
                Name = "RatPak 47",
                SpaceTimeLocationId = 0,
                Description =
                    "Standard issue ration package contain nutrients for 72 hours.",
                Type = TravelerObjectType.Food,
                Value = 0,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },

            new TravelerObject
            {
                Id = 10,
                Name = "Key to the first door",
                Description =
                    "A key to open the first door.",
                Type = TravelerObjectType.Key,
                Value = 0,
                UnlocksId = 1,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            },
        };

        public static List<NPC> Npcs = new List<NPC>()
        {
            new Survivor()
            {
                Name = "Survivor",
                Icon = "S",
                Messages = new List<string>
                {
                    "...save yourself..."
                }
            }
        };
    }
}
