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
            // prepare game play screen
            //
            // modify this method to remove action menu and add game map with player movement functionality
            _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrrentLocationInfo(), ActionMenu.MainMenu, "");

            //
            // draw the map
            //
            // SET 1,1 EQUAL TO PLAYER ICON (@)
            _gameConsoleView.drawMap();


            //
            // game loop
            //
            while (_playingGame)
            {

                //
                // get the coordinates of the player's current location on the map
                //
                int[] currentPlayerMapPosition = _gameMap.getCurrentPosition();
               
                //
                // player controls
                //
                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        // pass current position to update map method and update current position/mapLayout
                        _gameMap.updateMapLayout(currentPlayerMapPosition, keyInfo.Key);
                        // display the updated map
                        _gameConsoleView.displayUpdateMap(currentPlayerMapPosition);
                        break;

                    case ConsoleKey.DownArrow:
                        Traveler.moveDown();
                        break;

                    case ConsoleKey.LeftArrow:
                        Traveler.moveLeft();
                        break;

                    case ConsoleKey.RightArrow:
                        Traveler.moveRight();
                        break;
                }




                //
                // get next game action from player
                //
                // edit this method or create another method to get user movement input via 'asdf' or arrow keys
                //travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.MainMenu);

                //
                // choose an action based on the player's menu choice
                //
                //switch (travelerActionChoice)
                //{
                //    case TravelerAction.None:
                //        break;

                //    case TravelerAction.TravelerInfo:
                //        _gameConsoleView.DisplayTravelerInfo();
                //        break;

                //    case TravelerAction.Exit:
                //        _playingGame = false;
                //        break;

                //    default:
                //        break;
                // }
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
