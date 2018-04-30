using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    /// <summary>
    /// view class
    /// </summary>
    public class ConsoleView
    {
        #region ENUMS

        private enum ViewStatus
        {
            TravelerInitialization,
            PlayingGame
        }

        #endregion

        #region FIELDS

        //
        // declare game objects for the ConsoleView object to use
        //
        Traveler _gameTraveler;
        Universe _gameUniverse;
        Map _gameMap;
        System.Timers.Timer _gameTimer;

        ViewStatus _viewStatus;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView(Traveler gameTraveler, Universe gameUniverse, Map gameMap, System.Timers.Timer gameTimer)
        {
            _gameTimer = gameTimer;
            _gameTraveler = gameTraveler;
            _gameUniverse = gameUniverse;
            _gameMap = gameMap;

            _viewStatus = ViewStatus.TravelerInitialization;

            InitializeDisplay();
        }

        #endregion

        #region METHODS
        /// <summary>
        /// display all of the elements on the game play screen on the console
        /// </summary>
        /// <param name="messageBoxHeaderText">message box header title</param>
        /// <param name="messageBoxText">message box text</param>
        /// <param name="menu">menu to use</param>
        /// <param name="inputBoxPrompt">input box text</param>
        public void DisplayGamePlayScreen(string messageBoxHeaderText, string messageBoxText, Menu menu, string inputBoxPrompt)
        {
            //
            // reset screen to default window colors
            //
            Console.BackgroundColor = ConsoleTheme.WindowBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.WindowForegroundColor;
            Console.Clear();

            ConsoleWindowHelper.DisplayHeader(Text.HeaderText);
            ConsoleWindowHelper.DisplayFooter(Text.FooterText);

            DisplayMessageBox(messageBoxHeaderText, messageBoxText);
            DisplayMenuBox(menu);
            DisplayInputBox();
            DisplayStatusBox();
        }

        // Function to redraw the map
        public void DisplayRedrawMap(string messageBoxHeaderText, string messageBoxText, Menu menu, string inputBoxPrompt)
        {
            Console.Clear();

            ConsoleWindowHelper.DisplayHeader(Text.HeaderText);
            ConsoleWindowHelper.DisplayFooter(Text.FooterText);

            DisplayMessageBox(messageBoxHeaderText, messageBoxText);
            DisplayMenuBox(menu);
            DisplayInputBox();
            DisplayStatusBox();
        }


        // Function to display message in the 
        public void DisplayMessage(string theMessage)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 2);
            Console.ForegroundColor = ConsoleTheme.InputBoxErrorMessageForegroundColor;
            Console.Write(theMessage);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.CursorVisible = true;
        }

        // Function to display the updated timer
        public void DisplayUpdateTimer(int timeRemaining)
        {
            int startingRow = ConsoleLayout.StatusBoxPositionTop + 5;
            Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 3, startingRow);
            Console.Write("Time Remaining: " + timeRemaining);
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 2);
        }

        public void DisplayTalkTo(NPC npc)
        {
            ISpeak speakingNpc = npc as ISpeak;

            string message = npc.Name + ": \"" + speakingNpc.Speak() + "\"";

            //if (message == "")
            //{
            //    message = "You approach the survivor, but they have nothing to say...";
            //}

            // TODO
            DisplayGamePlayScreen("Speak to Character",message, ActionMenu.NpcMenu,"");
            DisplayMessage("Press any key to continue...");
            Console.ReadKey();
        }

        public void DisplayGameObjectInfo(GameObject gameObject, int[] playerPosition, string[,] map)
        {
            DisplayGamePlayScreen("Item", Text.LookAt(gameObject), ActionMenu.MainMenu, "");

            //
            // future change here -> add item menu and check if player wants to add to inventory or not
            //
            if (gameObject is TravelerObject)
            {
                TravelerObject travelerObject = gameObject as TravelerObject;
                if (travelerObject.CanInventory == true)
                {
                    _gameTraveler.Inventory.Add(travelerObject);

                    map[playerPosition[0], playerPosition[1]] = "-";

                    travelerObject.CanInventory = false;
                }
                else
                {

                }
            }

            DisplayMessage("Press any key to continue...");
        }

        public void DisplayItemReceived(NPC npc)
        {
            string message;

            IGiveItem givingNpc = npc as IGiveItem;

            List<TravelerObject>items = givingNpc.GiveItems();

            if (items == null || npc.HasItemsToGive == false)
            {
                message = "The survivor has nothing more to offer.";
                DisplayGamePlayScreen("Speak to Character", message, ActionMenu.NpcMenu, "");
                DisplayMessage("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                message = "You received the following item(s): ";

                foreach (TravelerObject item in items)
                {
                    _gameTraveler.Inventory.Add(item);
                    message += " \n" + item.Name;
                }

                npc.HasItemsToGive = false;

                DisplayGamePlayScreen("Speak to Character", message, ActionMenu.NpcMenu, "");
                DisplayMessage("Press any key to continue...");

                Console.ReadKey();
            }
        }

        public void DisplayNpcModifyMap(NPC npc, string[,] map)
        {

            // TODO -> handle case: npc doesn't have coords to update
            IModifyMap modifyingMapNpc = npc as IModifyMap;
            int[] coordsToUpdate = modifyingMapNpc.getCoordsToUpdate();

            map[coordsToUpdate[0],coordsToUpdate[1]] = "-";

        }

        public void DisplayListOfNPCs()
        {
            DisplayGamePlayScreen("List: NPCs", Text.ListAllNPCs(UniverseObjects.Npcs), ActionMenu.AdminMenu, "");
        }

        public void DisplayLocationsVisited()
        {
            //
            // generate a list of space time locations that have been visited
            //
            List<MapLocation> visitedRooms = new List<MapLocation>();
            foreach (MapLocation room in _gameTraveler.LocationsVisited)
            {
                visitedRooms.Add(room);
            }

            DisplayGamePlayScreen("Space-Time Locations Visited", Text.VisitedLocations(visitedRooms), ActionMenu.AdminMenu, "");
        }


        //
        // List all map locations
        //
        public void DisplayListAllMapLocations()
        {
            // display id and name of all map locations
            string messageBoxText = Text.ListAllMapLocations() + Environment.NewLine + Environment.NewLine;
            DisplayGamePlayScreen("Locations", messageBoxText, ActionMenu.AdminMenu, "");
        }


        public bool checkPlayerVisited(string nameOfLocation)
        {
            bool hasVisited = false;

            foreach (MapLocation location in _gameTraveler.LocationsVisited)
            {
                if (location.Name == nameOfLocation)
                {
                    hasVisited = true;
                }
                else
                {
                    hasVisited = false;
                }
            }

            return hasVisited;
        }

        //
        // List all game objects
        //
        public void DisplayListAllGameObjects()
        {
            // display id and name of all map locations
            string messageBoxText = Text.ListAllGameObjects() + Environment.NewLine + Environment.NewLine;
            DisplayGamePlayScreen("Game Objects", messageBoxText, ActionMenu.AdminMenu, "");
        }

        public void DisplayLookAround(string[,] map)
        {
            // get player position
            int[] playerCoords = new int[2];
            playerCoords = _gameMap.getCurrentPosition(map);

            // get the current room
            MapLocation currentMapRoom = _gameMap.getCurrentRoom(playerCoords);
            Console.WriteLine(currentMapRoom.Name);

            // define map search parameters
            int firstRow = playerCoords[0] - 1;
            int lastRow = playerCoords[0] + 2;

            int firstCol = playerCoords[1] - 1;
            int lastCol = playerCoords[1] + 2;

            //
            // check the map tiles around the player
            //
            // row above player to row below player
            List<GameObject> objectsNearby = new List<GameObject>();

            for (int r = firstRow; r < lastRow; r++)
            {
                // col left of player to col right of player
                for (int c = firstCol; c < lastCol; c++)
                {
                    foreach (GameObject gameObject in _gameUniverse.GameObjects)
                    {
                        if (gameObject.Icon == _gameMap.MapLayout[r,c])
                        {
                            objectsNearby.Add(gameObject);
                        }
                    }
                }
            }

            // display name and description of location as well as items nearby
            string messageBoxText = Text.LookAround(currentMapRoom, objectsNearby) + Environment.NewLine + Environment.NewLine;
            DisplayGamePlayScreen("Current Location", messageBoxText, ActionMenu.MainMenu, "");
        }


        /// <summary>
        /// wait for any keystroke to continue
        /// </summary>
        public void GetContinueKey()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// get a action menu choice from the user
        /// </summary>
        /// <returns>action menu choice</returns>
        public TravelerAction GetActionMenuChoice(Menu menu, ConsoleKeyInfo keyPressedInfo, string gameMapString)
        {
            TravelerAction chosenAction = TravelerAction.None;

            //
            // create an array of valid keys from menu dictionary
            //
            char[] validKeys = menu.MenuChoices.Keys.ToArray();

            //
            // validate key pressed as in MenuChoices dictionary
            //
            char keyPressed;
           
            keyPressed = keyPressedInfo.KeyChar;

            do
            {
                if (!validKeys.Contains(keyPressed))
                {
                    DisplayMessage("That's not a valid key! Try again or press a movement key to return to the map.");
                    keyPressedInfo = Console.ReadKey(true);
                    keyPressed = keyPressedInfo.KeyChar;
                }

                if ((keyPressedInfo.Key == ConsoleKey.UpArrow) || (keyPressedInfo.Key == ConsoleKey.DownArrow) || (keyPressedInfo.Key == ConsoleKey.LeftArrow) || (keyPressedInfo.Key == ConsoleKey.RightArrow))
                {
                    DisplayRedrawMap("Current Location", gameMapString, ActionMenu.MapMenu, "");
                    chosenAction = TravelerAction.None;
                    return chosenAction;
                }

            } while (!validKeys.Contains(keyPressed));
                
                
            chosenAction = menu.MenuChoices[keyPressed];
            

            return chosenAction;
        }

        /// <summary>
        /// get a string value from the user
        /// </summary>
        /// <returns>string value</returns>
        public string GetString()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// get an integer value from the user
        /// </summary>
        /// <returns>integer value</returns>
        public bool GetInteger(string prompt, int minimumValue, int maximumValue, out int integerChoice)
        {
            bool validResponse = false;
            integerChoice = 0;

            DisplayInputBoxPrompt(prompt);
            while (!validResponse)
            {
                if (int.TryParse(Console.ReadLine(), out integerChoice))
                {
                    if (integerChoice >= minimumValue && integerChoice <= maximumValue)
                    {
                        validResponse = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                        DisplayInputBoxPrompt(prompt);
                    }
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                    DisplayInputBoxPrompt(prompt);
                }
            }

            return true;
        }

        /// <summary>
        /// get a character race value from the user
        /// </summary>
        /// <returns>character race value</returns>
        public Character.RaceType GetRace()
        {
            Character.RaceType raceType;
            Enum.TryParse<Character.RaceType>(Console.ReadLine(), out raceType);

            return raceType;
        }

        /// <summary>
        /// get a companion name value from the user
        /// </summary>
        /// <returns>companion value</returns>
        public Traveler.CompanionName GetCompanionName()
        {
            Traveler.CompanionName companionName;
            Enum.TryParse<Traveler.CompanionName>(Console.ReadLine(), out companionName);
            

            return companionName;
        }

        /// <summary>
        /// display splash screen
        /// </summary>
        /// <returns>player chooses to play</returns>
        public bool DisplaySpashScreen()
        {
            bool playing = true;
            ConsoleKeyInfo keyPressed;

            Console.BackgroundColor = ConsoleTheme.SplashScreenBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.SplashScreenForegroundColor;
            Console.Clear();
            Console.CursorVisible = false;


            Console.SetCursorPosition(0, 10);
            string tabSpace = new String(' ', 35);
            Console.WriteLine(tabSpace + @"  ___  _ _              _____          _       _   _             ");
            Console.WriteLine(tabSpace + @" / _ \| (_)            |_   _|        | |     | | (_)            ");
            Console.WriteLine(tabSpace + @"/ /_\ \ |_  ___ _ __     | | ___  ___ | | __ _| |_ _  ___  _ __  ");
            Console.WriteLine(tabSpace + @"|  _  | | |/ _ \ '_ \    | |/ __|/ _ \| |/ _` | __| |/ _ \| '_ \ ");
            Console.WriteLine(tabSpace + @"| | | | | |  __/ | | |  _| |\__ \ (_) | | (_| | |_| | (_) | | | |");
            Console.WriteLine(tabSpace + @"\_| |_/_|_|\___|_| |_|  \___/___/\___/|_|\__,_|\__|_|\___/|_| |_|");
            Console.WriteLine(tabSpace + @"                                                                 ");
            Console.WriteLine(tabSpace + @"                         CONSOLE EDITION                         ");
            Console.SetCursorPosition(80, 25);
            Console.Write("Press any key to continue or Esc to exit.");
            keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                playing = false;
            }

            return playing;
        }

        public void DisplayGameOverScreen()
        {

            Console.BackgroundColor = ConsoleTheme.SplashScreenBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.SplashScreenForegroundColor;
            Console.Clear();
            Console.CursorVisible = false;


            Console.SetCursorPosition(0, 10);
            string tabSpace = new String(' ', 35);
            Console.WriteLine(tabSpace + @"   _____                         ____                 ");
            Console.WriteLine(tabSpace + @"  / ____|                       / __ \                ");
            Console.WriteLine(tabSpace + @" | |  __  __ _ _ __ ___   ___  | |  | |_   _____ _ __ ");
            Console.WriteLine(tabSpace + @" | | |_ |/ _` | '_ ` _ \ / _ \ | |  | \ \ / / _ \ '__|");
            Console.WriteLine(tabSpace + @" | |__| | (_| | | | | | |  __/ | |__| |\ V /  __/ |   ");
            Console.WriteLine(tabSpace + @"  \_____|\__,_|_| |_| |_|\___|  \____/  \_/ \___|_|   ");
            Console.WriteLine(tabSpace + @"                                                                 ");
            Console.SetCursorPosition(80, 25);
            Console.Write("Press any key to exit");


            Console.ReadLine();

            Environment.Exit(0);
        }

        public void drawMap()
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

            //int[] initialPosition = new int[] { 2, 2 };

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
        }

        public void displayUpdateMap(ConsoleKey keyDirection)
        {
            // iterate through map layout and find player icon. ( create map layout in map.cs )
            
            // use case statement to compare player key direction to respective array index value
            // if wall, display error message, else, set new current position
            // player.currentMapPosition = new Position;
            //player.previous position = "-";
            // update map.mapLayout and print the new layout 
        }

        /// <summary>
        /// initialize the console window settings
        /// </summary>
        private static void InitializeDisplay()
        {
            //
            // control the console window properties
            //
            ConsoleWindowControl.DisableResize();
            ConsoleWindowControl.DisableMaximize();
            ConsoleWindowControl.DisableMinimize();
            Console.Title = "The Aion Project";

            //
            // set the default console window values
            //
            ConsoleWindowHelper.InitializeConsoleWindow();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// display the correct menu in the menu box of the game screen
        /// </summary>
        /// <param name="menu">menu for current game state</param>
        private void DisplayMenuBox(Menu menu)
        {
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuBorderColor;

            //
            // display menu box border
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MenuBoxPositionTop,
                ConsoleLayout.MenuBoxPositionLeft,
                ConsoleLayout.MenuBoxWidth,
                ConsoleLayout.MenuBoxHeight);

            //
            // display menu box header
            //
            Console.BackgroundColor = ConsoleTheme.MenuBorderColor;
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(menu.MenuTitle, ConsoleLayout.MenuBoxWidth - 4));

            //
            // display menu choices
            //
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
            int topRow = ConsoleLayout.MenuBoxPositionTop + 3;

            foreach (KeyValuePair<char, TravelerAction> menuChoice in menu.MenuChoices)
            {
                if (menuChoice.Value != TravelerAction.None)
                {
                    string formatedMenuChoice = ConsoleWindowHelper.ToLabelFormat(menuChoice.Value.ToString());
                    Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 3, topRow++);
                    Console.Write($"{menuChoice.Key}. {formatedMenuChoice}");
                }
            }
        }

        /// <summary>
        /// display the text in the message box of the game screen
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="messageText"></param>
        private void DisplayMessageBox(string headerText, string messageText)
        {
            //
            // display the outline for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxBorderColor;
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MessageBoxPositionTop,
                ConsoleLayout.MessageBoxPositionLeft,
                ConsoleLayout.MessageBoxWidth,
                ConsoleLayout.MessageBoxHeight);

            //
            // display message box header
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBorderColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MessageBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(headerText, ConsoleLayout.MessageBoxWidth - 4));

            //
            // display the text for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxForegroundColor;
            List<string> messageTextLines = new List<string>();
            messageTextLines = ConsoleWindowHelper.MessageBoxWordWrap(messageText, ConsoleLayout.MessageBoxWidth - 4);

            int startingRow = ConsoleLayout.MessageBoxPositionTop + 3;
            int endingRow = startingRow + messageTextLines.Count();
            int row = startingRow;
            foreach (string messageTextLine in messageTextLines)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, row);
                Console.Write(messageTextLine);
                row++;
            }

        }

        /// <summary>
        /// draw the status box on the game screen
        /// </summary>
        public void DisplayStatusBox()
        {
            Console.BackgroundColor = ConsoleTheme.InputBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.InputBoxBorderColor;

            //
            // display the outline for the status box
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.StatusBoxPositionTop,
                ConsoleLayout.StatusBoxPositionLeft,
                ConsoleLayout.StatusBoxWidth,
                ConsoleLayout.StatusBoxHeight);

            //
            // display the text for the status box if playing game
            //
            if (_viewStatus == ViewStatus.PlayingGame)
            {
                //
                // display status box header with title
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("Game Stats", ConsoleLayout.StatusBoxWidth - 4));
                Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;

                //
                // display stats
                //
                //int startingRow = ConsoleLayout.StatusBoxPositionTop + 3;
                //int row = startingRow;
                //foreach (string statusTextLine in Text.StatusBox(_gameTraveler))
                //{
                //    Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 3, row);
                //    Console.Write(statusTextLine);
                //    row++;
                //}
            }
            else
            {
                //
                // display status box header without header
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("", ConsoleLayout.StatusBoxWidth - 4));
                Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
            }
        }

        /// <summary>
        /// draw the input box on the game screen
        /// </summary>
        public void DisplayInputBox()
        {
            Console.BackgroundColor = ConsoleTheme.InputBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.InputBoxBorderColor;

            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.InputBoxPositionTop,
                ConsoleLayout.InputBoxPositionLeft,
                ConsoleLayout.InputBoxWidth,
                ConsoleLayout.InputBoxHeight);
        }

        /// <summary>
        /// display the prompt in the input box of the game screen
        /// </summary>
        /// <param name="prompt"></param>
        public void DisplayInputBoxPrompt(string prompt)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 1);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.Write(prompt);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the error message in the input box of the game screen
        /// </summary>
        /// <param name="errorMessage">error message text</param>
        public void DisplayInputErrorMessage(string errorMessage)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 2);
            Console.ForegroundColor = ConsoleTheme.InputBoxErrorMessageForegroundColor;
            Console.Write(errorMessage);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the error message in the input box of the game screen
        /// </summary>
        /// <param name="errorMessage">error message text</param>
        public void ClearMessage()
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 2);
            Console.ForegroundColor = ConsoleTheme.InputBoxErrorMessageForegroundColor;
            Console.Write("");
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.CursorVisible = true;
        }

        /// <summary>
        /// clear the input box
        /// </summary>
        private void ClearInputBox()
        {
            string backgroundColorString = new String(' ', ConsoleLayout.InputBoxWidth - 4);

            Console.ForegroundColor = ConsoleTheme.InputBoxBackgroundColor;
            for (int row = 1; row < ConsoleLayout.InputBoxHeight - 2; row++)
            {
                Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + row);
                DisplayInputBoxPrompt(backgroundColorString);
            }
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
        }

        /// <summary>
        /// get the player's initial information at the beginning of the game
        /// </summary>
        /// <returns>player object with all properties updated</returns>
        public Traveler GetInitialTravelerInfo()
        {
            Traveler player = new Traveler();

            //
            // intro
            //
            DisplayGamePlayScreen("Mission Initialization", Text.InitializeMissionIntro(), ActionMenu.MissionIntro, "");
            GetContinueKey();

            //
            // get player's name
            //
            DisplayGamePlayScreen("Mission Initialization - Name", Text.InitializeMissionGetTravelerName(), ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt("Enter your name: ");
            player.Name = GetString();

            //
            // get player's age
            //
            DisplayGamePlayScreen("Mission Initialization - Age", Text.InitializeMissionGetTravelerAge(player.Name), ActionMenu.MissionIntro, "");
            int gameTravelerAge;

            GetInteger($"Enter your age {player.Name}: ", 0, 1000000, out gameTravelerAge);
            player.Age = gameTravelerAge;

            //
            // get player's race
            //
            DisplayGamePlayScreen("Mission Initialization - Race", Text.InitializeMissionGetTravelerRace(player), ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt($"Enter your race {player.Name}: ");
            player.Race = GetRace();

            //
            // get player's home planet
            //
            DisplayGamePlayScreen("Mission Initialization - Home Planet", Text.InitializeMissionGetTravelerHomePlanet(player.Name), ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt("Enter your home planet: ");
            player.HomePlanet = GetString();

            //
            // get name of player's companion
            //
            //DisplayGamePlayScreen("Mission Initialization - Companion Name", Text.InitializeMissionGetTravelerCompanionName(player), ActionMenu.MissionIntro, "");
            //DisplayInputBoxPrompt("Enter your Companion's name: ");
            //player.travelerCompanionName = GetCompanionName();

            //
            // echo the player's info
            //
            //DisplayGamePlayScreen("Mission Initialization - Complete", Text.InitializeMissionEchoTravelerInfo(player), ActionMenu.MissionIntro, "");
            //GetContinueKey();

            // 
            // change view status to playing game
            //
            _viewStatus = ViewStatus.PlayingGame;

            return player;
        }

        public void DisplayInventory()
        {
            DisplayGamePlayScreen("Current Inventory", Text.CurrentInventory(_gameTraveler.Inventory), ActionMenu.MainMenu, "");
        }

        #region ----- display responses to menu action choices -----

        public void DisplayTravelerInfo()
        {
            DisplayGamePlayScreen("Traveler Information", Text.TravelerInfo(_gameTraveler), ActionMenu.MainMenu, "");
        }

        #endregion

        #endregion
    }
}
