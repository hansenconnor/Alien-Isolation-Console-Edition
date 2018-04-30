using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TheAionProject
{
    /// <summary>
    /// controller for the MVC pattern in the application
    /// </summary>
    public class Controller
    {
        #region FIELDS
        private Universe _gameUniverse;
        private ConsoleView _gameConsoleView;
        private Traveler _gameTraveler;
        private Map _gameMap;
        private bool _playingGame;
        private System.Timers.Timer _gameTimer;
        private int timeRemaining;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            //
            // setup all of the objects in the game
            //
            InitializeGame();

            //
            // begins running the application UI
            //
            ManageGameLoop();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize the major game objects
        /// </summary>
        private void InitializeGame()
        {
            _gameTimer = new System.Timers.Timer();
            _gameTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _gameTimer.Interval = 1000;
            _gameTimer.Enabled = true;
            timeRemaining = 180;
            _gameTimer.Stop();


            _gameTraveler = new Traveler();
            _gameUniverse = new Universe();
            _gameMap = new Map();

            _gameConsoleView = new ConsoleView(_gameTraveler, _gameUniverse, _gameMap, _gameTimer);
            
            _playingGame = true;

            //
            // add initial items to the traveler's inventory
            //
            //_gameTraveler.Inventory.Add(_gameUniverse.GetGameObjectById(1) as TravelerObject);
            _gameTraveler.LocationsVisited.Add(UniverseObjects.mapLocations[0]);

            Console.CursorVisible = false;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private  void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (timeRemaining == 0)
            {
                _playingGame = false;
                _gameTimer.Stop();
                _gameConsoleView.DisplayGameOverScreen();
            }
            else
            {
                _gameConsoleView.DisplayUpdateTimer(timeRemaining);
                timeRemaining--;
            }
            
        }


        /// <summary>
        /// method to manage the application setup and game loop
        /// </summary>
        private void ManageGameLoop()
        {
            TravelerAction travelerActionChoice = TravelerAction.None;

            //
            // display splash screen
            //
            _playingGame = _gameConsoleView.DisplaySpashScreen();

            //
            // player chooses to quit
            //
            if (!_playingGame)
            {
                Environment.Exit(1);
            }

            //
            // display introductory message
            //

            _gameConsoleView.DisplayGamePlayScreen("Mission Intro", Text.MissionIntro(), ActionMenu.MissionIntro, "");
            _gameConsoleView.GetContinueKey();

            //
            // initialize the mission traveler
            // 
            InitializeMission();

            //
            // store initial map layout in 2D array
            //
            // string[,] gameMapArray = _gameMap.drawMap();
            _gameMap.MapLayout = _gameMap.drawMap();

            //
            // convert initial map array to string
            // 
            string gameMapString = _gameMap.convertMapToString(_gameMap.MapLayout);

            //
            // create array to hold player row and col coords
            // then get initial player position
            int[] currentPlayerMapPosition = new int[2];
            currentPlayerMapPosition = _gameMap.getCurrentPosition(_gameMap.MapLayout);

            MapLocation currentPlayerRoom;

            //
            // print the map
            //
            //
            _gameConsoleView.DisplayGamePlayScreen("Current Location", gameMapString, ActionMenu.MapMenu, "");


            _gameTimer.Start();
            //
            // game loop
            //
            while (_playingGame)
            {
                // player controls
                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey(true);

                // validate keys
                // if not valid key - error message

                // check for player movement key TODO -> validate and make sure player isn't in menu
                if ((keyInfo.Key == ConsoleKey.UpArrow) || (keyInfo.Key == ConsoleKey.DownArrow) || (keyInfo.Key == ConsoleKey.LeftArrow) || (keyInfo.Key == ConsoleKey.RightArrow))
                {
                    // get the players current position
                    currentPlayerMapPosition = _gameMap.getCurrentPosition(_gameMap.MapLayout);
                    currentPlayerRoom = _gameMap.getCurrentRoom(currentPlayerMapPosition);

                    // check if the player has entered a certain room and then modify a metric
                    bool hasVisited = _gameConsoleView.checkPlayerVisited("Engine Room 2");

                    


                    if (currentPlayerRoom.Name == "Engine Room 2")
                    {
                        if (!hasVisited)
                        {
                            timeRemaining -= 10;
                            _gameConsoleView.DisplayMessage("The alien heard you! 10 Less seconds to escape!! Press any key to continue...");
                            Console.ReadKey();
                        }
                    }

                    // get the icon for the desired tile
                    int [] nextTile = _gameMap.getTile(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);

                    // check if player has reached end of game tile
                    if ((nextTile[0]) == 18 && (nextTile[1] == 44))
                    {
                        _gameTimer.Stop();
                        _gameConsoleView.DisplayGameSuccessScreen(timeRemaining);
                    }

                    // refactor update map to check if desired position is available or if there is an NPC or item etc.
                    // refactor update map to return bool 
                    //bool validTile = _gameMap.validateCellType(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);

                    //
                    // determine object in cell type
                    //
                    if (_gameMap.MapLayout[nextTile[0], nextTile[1]] == "#")
                    {
                        _gameConsoleView.DisplayInputErrorMessage("That's a wall! You can't go that way!");
                    }
                    else if (_gameMap.MapLayout[nextTile[0], nextTile[1]] == "-")
                    {
                        // check if new location and update locations visited list if so
                        MapLocation currentMapRoom = _gameMap.getCurrentRoom(currentPlayerMapPosition);
                        if (!_gameTraveler.LocationsVisited.Contains(currentMapRoom))
                        {
                            _gameTraveler.LocationsVisited.Add(currentMapRoom);
                        }


                        // update the game map array
                        _gameMap.MapLayout = _gameMap.updateMap(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);

                        // update the game map string
                        gameMapString = _gameMap.convertMapToString(_gameMap.MapLayout);

                        // display updated map
                        _gameConsoleView.DisplayRedrawMap("Current Location", gameMapString, ActionMenu.MapMenu, "");
                    }
                    else if ((_gameMap.MapLayout[nextTile[0], nextTile[1]] == "|") || (_gameMap.MapLayout[nextTile[0], nextTile[1]] == "_"))
                    {
                        int[] doorCoords = new int[2];
                        doorCoords[0] = nextTile[0];
                        doorCoords[1] = nextTile[1];
                        bool hasKey = false;

                        foreach (TravelerObject travelerObject in _gameTraveler.Inventory)
                        {
                            if (travelerObject.Type == TravelerObjectType.Key)
                            {
                                foreach (MapObject door in UniverseObjects.mapObjects)
                                {
                                    // check if the door coordinates correspond to the coordinates of the next tile
                                    if ((door.Coords[0] == doorCoords[0]) && (door.Coords[1] == doorCoords[1]))
                                    {
                                        // 
                                        // check if the key unlocks id matches the door id
                                        if (travelerObject.UnlocksId == door.Id)
                                        {
                                            hasKey = true;
                                            //Console.WriteLine("asdf asd aSD");
                                            // player has key to open door

                                            _gameMap.MapLayout[nextTile[0], nextTile[1]] = "@";

                                            // update the game map array
                                            _gameMap.MapLayout = _gameMap.updateMap(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);

                                            // update the game map string
                                            gameMapString = _gameMap.convertMapToString(_gameMap.MapLayout);

                                            _gameConsoleView.DisplayRedrawMap("Current Location", gameMapString, ActionMenu.MapMenu, "");
                                        }
                                    }
                                }
                            }
                        }
                        if (!hasKey)
                        {
                            _gameConsoleView.DisplayInputErrorMessage("You need a key to unlock this door!");
                        }
                        
                        // check if object is key

                        //command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    }
                    else if (_gameMap.MapLayout[nextTile[0], nextTile[1]] == "K")
                    {
                        int[] keyCoords = new int[2];
                        keyCoords[0] = nextTile[0];
                        keyCoords[1] = nextTile[1];
                        foreach (TravelerObject key in UniverseObjects.gameObjects)
                        {
                            if (key.Coords != null)
                            {
                                if ((key.Coords[0] == keyCoords[0]) && (key.Coords[1] == keyCoords[1]))
                                {
                                    _gameConsoleView.DisplayGameObjectInfo(key, nextTile, _gameMap.MapLayout);
                                }
                            } else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        int[] npcCoords = new int[2];
                        npcCoords[0] = nextTile[0];
                        npcCoords[1] = nextTile[1];
                        // display the NPC menu
                        foreach (NPC npc in _gameUniverse.Npcs)
                        {
                            // check if the coordinates match
                            if ((npc.Coords[0] == npcCoords[0]) && (npc.Coords[1] == npcCoords[1]))
                            {
                                if (npc.Icon == _gameMap.MapLayout[nextTile[0], nextTile[1]])
                                {
                                    if (npc is ISpeak)
                                    {
                                        // display greeting
                                        _gameConsoleView.DisplayTalkTo(npc);
                                    }
                                    if (npc is IGiveItem)
                                    {
                                        _gameConsoleView.DisplayItemReceived(npc);
                                    }
                                    if (npc is IModifyMap)
                                    {
                                        _gameConsoleView.DisplayNpcModifyMap(npc, _gameMap.MapLayout);
                                    }
                                    ActionMenu.currentMenu = ActionMenu.CurrentMenu.MapMenu;
                                    _gameConsoleView.DisplayRedrawMap("Current Location", gameMapString, ActionMenu.MapMenu, "");

                                    //
                                    // call function to handle NPC interaction
                                    // npcInteraction(npc);
                                    //
                                }
                            }
                        }
                    }
                }

                else
                {
                    if (ActionMenu.currentMenu == ActionMenu.CurrentMenu.MapMenu)
                    {
                        travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.MapMenu, keyInfo, gameMapString);
                    }
                    else if (ActionMenu.currentMenu == ActionMenu.CurrentMenu.MainMenu)
                    {
                        travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.MainMenu, keyInfo, gameMapString);
                    }
                    else if (ActionMenu.currentMenu == ActionMenu.CurrentMenu.NPCMenu)
                    {
                        travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.NpcMenu, keyInfo, gameMapString);
                    }
                    else if (ActionMenu.currentMenu == ActionMenu.CurrentMenu.AdminMenu)
                    {
                        travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.AdminMenu, keyInfo, gameMapString);
                    }

                    switch (travelerActionChoice)
                    {
                        case TravelerAction.None:
                            break;

                        case TravelerAction.TalkTo:
                            //_gameConsoleView.DisplayTalkTo(npc);
                            break;

                        case TravelerAction.ReturnToMap:
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.MapMenu;
                            _gameConsoleView.DisplayRedrawMap("Current Location", gameMapString, ActionMenu.MapMenu, "");
                            break;

                        case TravelerAction.TravelerInfo:
                            _gameConsoleView.DisplayTravelerInfo();
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                            break;

                        case TravelerAction.LookAround:
                            _gameConsoleView.DisplayLookAround(_gameMap.MapLayout);
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                            break;

                        case TravelerAction.AdminMenu:
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.AdminMenu;
                            _gameConsoleView.DisplayGamePlayScreen("Admin Menu", "Select an operation from the menu.", ActionMenu.AdminMenu, "");
                            break;

                        case TravelerAction.Inventory:
                            _gameConsoleView.DisplayInventory();
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                            break;

                        case TravelerAction.ListAllNpcs:
                            _gameConsoleView.DisplayListOfNPCs();
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.AdminMenu;
                            break;

                        case TravelerAction.ListGameObjects:
                            _gameConsoleView.DisplayListAllGameObjects();
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.AdminMenu;
                            break;

                        case TravelerAction.ListLocationsVisited:
                            _gameConsoleView.DisplayLocationsVisited();
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.AdminMenu;
                            break;

                        case TravelerAction.ListAllLocations:
                            _gameConsoleView.DisplayListAllMapLocations();
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.AdminMenu;
                            break;

                        case TravelerAction.Exit:
                            _playingGame = false;
                            break;

                        default:
                            break;
                    }
                }
            }
            //
            // close the application
            //
            Environment.Exit(1);
        }

        /// <summary>
        /// initialize the player info
        /// </summary>
        private void InitializeMission()
        {
            Traveler traveler = _gameConsoleView.GetInitialTravelerInfo();

            _gameTraveler.Name = traveler.Name;
            _gameTraveler.Age = traveler.Age;
            _gameTraveler.Race = Character.RaceType.Human;
            _gameTraveler.HomePlanet = traveler.HomePlanet;
            _gameTraveler.SpaceTimeLocationID = 1;
            _gameTraveler.travelerCompanionName = traveler.travelerCompanionName;
            if (_gameTraveler.HomePlanet == "Earth" || _gameTraveler.HomePlanet == "earth")
            {
                _gameTraveler.EarthBorn = true;
            }       
            //
            // echo the traveler's info
            //
            _gameConsoleView.DisplayGamePlayScreen("Mission Initialization - Complete", Text.InitializeMissionEchoTravelerInfo(_gameTraveler), ActionMenu.MissionIntro, "");

            // call displaygameplayscreen and pass player location as header text. Also draw/update map

            _gameConsoleView.GetContinueKey();
        }

        #endregion
    }
}
