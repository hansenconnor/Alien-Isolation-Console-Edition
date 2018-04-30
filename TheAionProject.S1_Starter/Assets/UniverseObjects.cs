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
                Id = 1,
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
                Id = 2,
                Icon = "G",
                Name = "Bag of Gold",
                RoomId = 2,
                Description = "A small leather pouch filled with 9 gold coins.",
                Type = TravelerObjectType.Treasure,
                Value = 45,
                CanInventory = true,
                IsConsumable = true,
                IsVisible = true
            },
        };

        public static List<NPC> Npcs = new List<NPC>()
        {
            new Survivor()
            {
                Name = "Survivor",
                Coords = new int[2] { 4,8 },
                Icon = "S",
                Messages = new List<string>
                {
                    "...save yourself..."
                },
                ItemsToGive = new List<TravelerObject>
                {
                    new TravelerObject
                    {
                        Id = 1,
                        Coords = new int[2] { 2, 2 },
                        Icon = "K",
                        Name = "Key to the first door",
                        Description =
                            "A key to open the first door.",
                        Type = TravelerObjectType.Key,
                        Value = 0,
                        UnlocksId = 1,
                        CanInventory = true,
                        IsConsumable = false,
                        IsVisible = true
                    }
                }
            },

            new Survivor()
            {
                Name = "Survivor",
                Coords = new int[2] { 4,2 },
                Icon = "S"
            }
        };
    }
}
