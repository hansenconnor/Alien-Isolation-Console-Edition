﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    /// <summary>
    /// class to store static and to generate dynamic text for the message and input boxes
    /// </summary>
    public static class Text
    {
        public static List<string> HeaderText = new List<string>() { "Alien Isolation - Console Edition" };
        public static List<string> FooterText = new List<string>() { "Connor Hansen, 2018" };

        #region INTITIAL GAME SETUP

        public static string MissionIntro()
        {
            string messageBoxText =
            "You have narrowly escaped the alien and now you must escape the space station. " +
            "Work your way to the bridge and reset the controls for the esape pods " +
            "so you can make your escape. " +
            " \n" +
            "Press the Esc key to exit the game at any point.\n" +
            " \n" +
            "Your mission begins now.\n" +
            " \n" +
            "\tYour first task will be to set up the initial parameters of your mission.\n" +
            " \n" +
            "\tPress any key to begin the Mission Initialization Process.\n";

            return messageBoxText;
        }

        public static string CurrrentLocationInfo()
        {
            string messageBoxText =
            "You are now in the Norlon Corporation research facility located in " +
            "the city of Heraklion on the north coast of Crete. You have passed through " +
            "heavy security and descended an unknown number of levels to the top secret " +
            "research lab for the Aion Project.\n" +
            " \n" +
            "\tChoose from the menu options to proceed.\n";

            return messageBoxText;
        }

        public static string InitializeMissionGetTravelerHomePlanet ( string name )
        {
            string messageBoxText =
                $"{name}, in case of emergency, it may be necessary to return your remains home.\n" +
                " \n" +
                "Enter your home planet below.\n" +
                " \n" +
                "Please use the standard Galactic designation as your reference";

            return messageBoxText;
        }

        #region Initialize Mission Text

        public static string InitializeMissionIntro()
        {
            string messageBoxText =
                "Before you begin your mission we much gather your base data.\n" +
                " \n" +
                "You will be prompted for the required information. Please enter the information below.\n" +
                " \n" +
                "\tPress any key to begin.";

            return messageBoxText;
        }

        public static string InitializeMissionGetTravelerName()
        {
            string messageBoxText =
                "Enter your name traveler.\n" +
                " \n" +
                "Please use the name you wish to be referred during your mission.";

            return messageBoxText;
        }

        public static string InitializeMissionGetTravelerAge(string name)
        {
            string messageBoxText =
                $"Very good then, we will call you {name} on this mission.\n" +
                " \n" +
                "Enter your age below.\n" +
                " \n" +
                "Please use the standard Earth year as your reference.";

            return messageBoxText;
        }

        public static string InitializeMissionGetTravelerRace(Traveler gameTraveler)
        {
            string messageBoxText =
                $"{gameTraveler.Name}, it will be important for us to know your race on this mission.\n" +
                " \n" +
                "Enter your race below.\n" +
                " \n" +
                "Please use the universal race classifications below." +
                " \n";

            string raceList = null;

            foreach (Character.RaceType race in Enum.GetValues(typeof(Character.RaceType)))
            {
                if (race != Character.RaceType.None)
                {
                    raceList += $"\t{race}\n";
                }
            }

            messageBoxText += raceList;

            return messageBoxText;
        }

        public static string InitializeMissionGetTravelerCompanionName(Traveler gameTraveler)
        {
            string messageBoxText =
                $"{gameTraveler.Name}, it will be important for us to know your companion's name on this mission.\n" +
                " \n" +
                "Enter your companion's name below.\n" +
                " \n" +
                " \n";

            string companionNameList = null;

            foreach (Traveler.CompanionName companionName in Enum.GetValues(typeof(Traveler.CompanionName)))
            {
                if (companionName != Traveler.CompanionName.None)
                {
                    companionNameList += $"\t{companionName}\n";
                }
            }

            messageBoxText += companionNameList;

            return messageBoxText;
        }

        public static string InitializeMissionEchoTravelerInfo(Traveler gameTraveler)
        {
            string messageBoxText =
                $"Very good then {gameTraveler.Name}.\n" +
                " \n" +
                "It appears we have all the necessary data to begin your mission. You will find it" +
                " listed below.\n" +
                " \n" +
                $"\tTraveler Name: {gameTraveler.Name}\n" +
                $"\tTraveler Age: {gameTraveler.Age}\n" +
                $"\tTraveler Race: {gameTraveler.Race}\n" +
                $"\tTraveler Home Planet: {gameTraveler.HomePlanet}\n" +
                $"\tTraveler Greeting: {gameTraveler.Greeting()}\n" +
                $"\tEarthborn Status: {gameTraveler.EarthBorn}\n" +
                $"\tCompanion Name: {gameTraveler.travelerCompanionName}\n" +
                " \n" +
                "Press any key to begin your mission.";

            return messageBoxText;
        }

        #endregion

        #endregion

        #region MAIN MENU ACTION SCREENS

        public static string TravelerInfo(Traveler gameTraveler)
        {
            string messageBoxText =
                $"\tTraveler Name: {gameTraveler.Name}\n" +
                $"\tTraveler Age: {gameTraveler.Age}\n" +
                $"\tTraveler Race: {gameTraveler.Race}\n" +
                $"\tTraveler Home Planet: {gameTraveler.HomePlanet}\n" +
                $"\tTraveler Home Planet: {gameTraveler.travelerCompanionName}\n" +
                $"\tTraveler Greeting: {gameTraveler.Greeting()}\n" +
                " \n";

            return messageBoxText;
        }

        #endregion

        public static List<string> StatusBox(Traveler traveler)
        {
            List<string> statusBoxText = new List<string>();

            statusBoxText.Add($"Traveler's Age: {traveler.Age}\n");

            return statusBoxText;
        }

        public static string ListAllNPCs(List<NPC> NPCs)
        {
            string messageBoxText = "";

            messageBoxText =
            "ID".PadRight(10) +
            "Name".PadRight(30) +
            "Description".PadRight(10) +
            "\n" +
            "---".PadRight(10) +
            "----------------------------".PadRight(30) +
            "----------------------".PadRight(10) +
            "\n";

            string inventoryObjectRows = null;
            foreach (NPC npc in NPCs)
            {
                inventoryObjectRows +=
                    $"{npc.Id}".PadRight(10) +
                    $"{npc.Name}".PadRight(30) +
                    $"{npc.Description}".PadRight(10) +
                    Environment.NewLine;
            }

            messageBoxText += inventoryObjectRows;

            return messageBoxText;
        }

        public static string LookAround(MapLocation currentRoom)
        {
            string messageBoxText =
                $"Current Location: {currentRoom.Name}\n" +
                " \n" +
                currentRoom.Description;

            return messageBoxText;
        }

        public static string CurrentInventory(IEnumerable<TravelerObject> inventory)
        {
            string messageBoxText = "";

            //
            // display table header
            //
            messageBoxText =
            "ID".PadRight(10) +
            "Name".PadRight(30) +
            "Type".PadRight(10) +
            "\n" +
            "---".PadRight(10) +
            "----------------------------".PadRight(30) +
            "----------------------".PadRight(10) +
            "\n";

            //
            // display all traveler objects in rows
            //
            string inventoryObjectRows = null;
            foreach (TravelerObject inventoryObject in inventory)
            {
                inventoryObjectRows +=
                    $"{inventoryObject.Id}".PadRight(10) +
                    $"{inventoryObject.Name}".PadRight(30) +
                    $"{inventoryObject.Type}".PadRight(10) +
                    Environment.NewLine;
            }

            messageBoxText += inventoryObjectRows;

            return messageBoxText;
        }
    }
}
