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
                    { '3', TravelerAction.Exit }
                }
        };

        public static Menu MapMenu = new Menu()
        {
            MenuName = "MapMenu",
            MenuTitle = "Map Menu",
            MenuChoices = new Dictionary<char, TravelerAction>()
                {
                    { '1', TravelerAction.TravelerInfo },
                    { '2', TravelerAction.Exit }
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
                    { '0', TravelerAction.ReturnToMainMenu }
                }
        };
    }
}
