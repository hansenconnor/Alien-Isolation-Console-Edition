using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _gameTraveler = new Traveler();
            _gameUniverse = new Universe();
            _gameMap = new Map();

            _gameConsoleView = new ConsoleView(_gameTraveler, _gameUniverse, _gameMap);
            
            _playingGame = true;

            //
            // add initial items to the traveler's inventory
            //
            //_gameTraveler.Inventory.Add(_gameUniverse.GetGameObjectById(1) as TravelerObject);

            Console.CursorVisible = false;
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

            //
            // print the map
            //
            //
            _gameConsoleView.DisplayGamePlayScreen("Current Location", gameMapString, ActionMenu.MapMenu, "");

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

                    // get the icon for the desired tile
                    int [] nextTile = _gameMap.getTile(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);

                    // refactor update map to check if desired position is available or if there is an NPC or item etc.
                    // refactor update map to return bool 
                    //bool validTile = _gameMap.validateCellType(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);
                    
                    //
                    // determine object in cell type
                    //
                    if (_gameMap.MapLayout[nextTile[0],nextTile[1]] == "#")
                    {
                        _gameConsoleView.DisplayInputErrorMessage("That's a wall! You can't go that way!");
                    }
                    else if (_gameMap.MapLayout[nextTile[0], nextTile[1]] == "-")
                    {
                        // update the game map array
                        _gameMap.MapLayout = _gameMap.updateMap(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);

                        // update the game map string
                        gameMapString = _gameMap.convertMapToString(_gameMap.MapLayout);

                        // display updated map
                        _gameConsoleView.DisplayRedrawMap("Current Location", gameMapString, ActionMenu.MapMenu, "");
                    }
                    else if (_gameMap.MapLayout[nextTile[0], nextTile[1]] == "|")
                    {
                        int[] doorCoords = new int[2];
                        doorCoords[0] = nextTile[0];
                        doorCoords[1] = nextTile[1];

                        foreach (TravelerObject travelerObject in _gameTraveler.Inventory)
                            // check if object is key
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
                                            //Console.WriteLine("asdf asd aSD");
                                            // player has key to open door

                                            _gameMap.MapLayout[nextTile[0], nextTile[1]] = "@";

                                            // update the game map array
                                            _gameMap.MapLayout = _gameMap.updateMap(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);

                                            // update the game map string
                                            gameMapString = _gameMap.convertMapToString(_gameMap.MapLayout);

                                            _gameConsoleView.DisplayRedrawMap("Current Location", gameMapString, ActionMenu.MapMenu, "");
                                        }
                                        else { _gameConsoleView.DisplayInputErrorMessage("You don't have a key that unlocks this door!"); };
                                    }
                                }
                            }else { _gameConsoleView.DisplayInputErrorMessage("You need a key to unlock this door!"); }
                            //command.Parameters.AddWithValue(kvp.Key, kvp.Value);
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
                                    _gameConsoleView.DisplayGamePlayScreen("NPC Menu", "Select an operation from the menu.", ActionMenu.NpcMenu, "");
                                    ActionMenu.currentMenu = ActionMenu.CurrentMenu.NPCMenu;
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
                            ActionMenu.currentMenu = ActionMenu.CurrentMenu.MapMenu;
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
            _gameTraveler.Race = traveler.Race;
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
