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
                Bounds = new MapLocation.LocationBounds(1,1,5,9)
            },
            new MapLocation
            {
                ID = 2,
                Name = "Service Hallway",
                Bounds = new MapLocation.LocationBounds(1,11,5,50)
            },
            new MapLocation
            {
                ID = 3,
                Name = "Service Cooridor",
                Bounds = new MapLocation.LocationBounds(7,1,10,36)
            },
            new MapLocation
            {
                ID = 4,
                Name = "Engine Room 1",
                Bounds = new MapLocation.LocationBounds(12,1,18,14)
            },
            new MapLocation
            {
                ID = 5,
                Name = "Engine Room 2",
                Bounds = new MapLocation.LocationBounds(12,20,18,36)
            },
            new MapLocation
            {
                ID = 6,
                Name = "Evacuation Cooridor",
                Bounds = new MapLocation.LocationBounds(7,38,18,50)
            }
            // TODO add 4 more map locations
        };

        public static List<GameObject> gameObjects = new List<GameObject>()
        {
            new TravelerObject
            {
                Id = 3,
                Coords = new int[2] { 1, 2 },
                Icon = "K",
                Name = "Test key",
                Description =
                    "A test key",
                Type = TravelerObjectType.Key,
                Value = 0,
                UnlocksId = 1,
                CanInventory = true,
                IsConsumable = false,
                IsVisible = true
            }
        };

        public static List<NPC> Npcs = new List<NPC>()
        {
            new Survivor()
            {
                Id = 1,
                Name = "Survivor",
                Coords = new int[2] { 4,8 },
                Icon = "S",
                Messages = new List<string>
                {
                    "...save yourself..."
                },
                HasItemsToGive = true,
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
                },
                Description = "A survivor cluching what appears to be a key card."
            },
            new Survivor()
            {
                Id = 2,
                Name = "Survivor",
                Coords = new int[2] { 4,2 },
                Icon = "S",
                HasItemsToGive = false,
                Description = "An injured surviror laying on the floor."
            },
            new Engineer()
            {
                Id = 3,
                Name="Engine Room Engineer",
                Coords = new int[2] { 15,2 },
                Icon = "E",
                Description = "An engineer operating some sort of computer interface."
            },
            new Engineer()
            {
                Id = 4,
                Name="Engine Room Engineer 2",
                Coords = new int[2] { 2,4 },
                DoorToUnlockCoords = new int[2] { 15,19 },
                Icon = "E",
                Description = "An Engineer sending an S.O.S via a control console."
            },

        };
    }
}
