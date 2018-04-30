using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    /// <summary>
    /// static class to hold key/value pairs for menu options
    /// </summary>
    public static class ActionMenu
    {
        public enum CurrentMenu
        {
            MissionIntro,
            InitializeMission,
            MainMenu,
            NPCMenu,
            MapMenu,
            AdminMenu,
            ItemMenu
        }

        // set the initial menu as Main Menu
        public static CurrentMenu currentMenu = CurrentMenu.MapMenu;

        public static Menu NpcMenu = new Menu()
        {
            MenuName = "NpcMenu",
            MenuTitle = "NPC Menu",
            MenuChoices = new Dictionary<char, TravelerAction>()
                    {
                        { '1', TravelerAction.TalkTo },
                        { '2', TravelerAction.ReturnToMap }
                    }
        };

        public static Menu ItemMenu = new Menu()
        {
            MenuName = "ItemMenu",
            MenuTitle = "",
            MenuChoices = new Dictionary<char, TravelerAction>()
                    {
                        { ' ', TravelerAction.None }
                    }
        };

        public static Menu MissionIntro = new Menu()
        {
            MenuName = "MissionIntro",
            MenuTitle = "",
            MenuChoices = new Dictionary<char, TravelerAction>()
                    {
                        { ' ', TravelerAction.None }
                    }
        };

        public static Menu InitializeMission = new Menu()
        {
            MenuName = "InitializeMission",
            MenuTitle = "Initialize Mission",
            MenuChoices = new Dictionary<char, TravelerAction>()
                {
                    { '1', TravelerAction.Exit }
                }
        };

        public static Menu MainMenu = new Menu()
        {
            MenuName = "MainMenu",
            MenuTitle = "Main Menu",
            MenuChoices = new Dictionary<char, TravelerAction>()
                {
                    { '1', TravelerAction.TravelerInfo },
                    { '2', TravelerAction.ReturnToMap },
                    { '3', TravelerAction.Inventory },
                    { '4', TravelerAction.Exit },
                    
                    //{ '2', TravelerAction.LookAround },
                    //{ '3', TravelerAction.LookAt },
                    //{ '4', TravelerAction.PickUp },
                    //{ '5', TravelerAction.PutDown },
                    //{ '7', TravelerAction.Travel },
                    //{ '8', TravelerAction.TravelerLocationsVisited },
                    //{ '9', TravelerAction.AdminMenu }
                }
        };

        public static Menu MapMenu = new Menu()
        {
            MenuName = "MapMenu",
            MenuTitle = "Map Menu",
            MenuChoices = new Dictionary<char, TravelerAction>()
                {
                    { '1', TravelerAction.TravelerInfo },
                    { '2', TravelerAction.Inventory },
                    { '3', TravelerAction.LookAround },
                    { '4', TravelerAction.AdminMenu },
                    { '5', TravelerAction.Exit }
                }
        };

        public static Menu AdminMenu = new Menu()
        {
            MenuName = "AdminMenu",
            MenuTitle = "Admin Menu",
            MenuChoices = new Dictionary<char, TravelerAction>()
                {
                    { '1', TravelerAction.ListSpaceTimeLocations },
                    { '2', TravelerAction.ListGameObjects},
                    { '3', TravelerAction.ListAllNpcs},
                    { '4', TravelerAction.ListLocationsVisited },
                    { '0', TravelerAction.ReturnToMap }
                }
        };
    }
}
