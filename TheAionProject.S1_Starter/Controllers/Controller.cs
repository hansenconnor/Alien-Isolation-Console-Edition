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
            _gameConsoleView = new ConsoleView(_gameTraveler);
            _gameMap = new Map();
            _playingGame = true;

            Console.CursorVisible = false;
        }

        /// <summary>
        /// method to manage the application setup and game loop
        /// </summary>
        private void ManageGameLoop()
        {
            //TravelerAction travelerActionChoice = TravelerAction.None;

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
            // modify this method to remove action menu and add game map with player movement functionality
            _gameConsoleView.DisplayGamePlayScreen("Current Location", gameMapString, ActionMenu.MainMenu, "");

            //
            // set initial menu status
            bool inMenu = false;

            //
            // game loop
            //
            while (_playingGame)
            {
                //
                // player controls
                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey(true);

                //
                // validate key from list of valid keys
                //while (!KeyStrokes.validKeyStrokes.Contains(keyInfo.Key))
                //{
                //    _gameConsoleView.DisplayInputErrorMessage("That's not a valid key! Please try again...");
                //}
                //
                // key is valid -> handle key
                _gameMap.handleKeyStroke(keyInfo.Key);

                //
                // move this to handleKeyStroke method inside of the Map class
                if ((keyInfo.Key == ConsoleKey.UpArrow) || (keyInfo.Key == ConsoleKey.DownArrow) || (keyInfo.Key == ConsoleKey.LeftArrow) || (keyInfo.Key == ConsoleKey.RightArrow))
                {
                    // get the players current position
                    currentPlayerMapPosition = _gameMap.getCurrentPosition(_gameMap.MapLayout);

                    // update the game map array
                    _gameMap.MapLayout = _gameMap.updateMap(_gameMap.MapLayout, currentPlayerMapPosition, keyInfo.Key);

                    // update the game map string
                    gameMapString = _gameMap.convertMapToString(_gameMap.MapLayout);

                    // display updated map
                    _gameConsoleView.DisplayRedrawMap("Current Location", gameMapString, ActionMenu.MainMenu, "");
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
