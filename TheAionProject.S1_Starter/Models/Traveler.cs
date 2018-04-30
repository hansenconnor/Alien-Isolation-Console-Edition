using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    /// <summary>
    /// the character class the player uses in the game
    /// </summary>
    public class Traveler : Character
    {
        #region ENUMERABLES
        public enum CompanionName
        {
            None,
            Mike,
            Susan,
            Katie,
            Jack
        }

        #endregion

        #region FIELDS
        private string _homePlanet;
        private CompanionName _travelerCompanionName;
        private bool _earthBorn;
        private List<TravelerObject> _inventory;
        private List<MapLocation> _locationsVisited;

        // int double
        #endregion


        #region PROPERTIES

        public List<MapLocation> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }
        public bool EarthBorn
        {
            get { return _earthBorn; }
            set { _earthBorn = value; }
        }

        public string HomePlanet
        {
            get { return _homePlanet; }
            set { _homePlanet = value; }
        }

        public CompanionName travelerCompanionName
        {
            get { return _travelerCompanionName; }
            set { _travelerCompanionName = value; }
        }

        public List<TravelerObject> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }


        #endregion


        #region CONSTRUCTORS

        public Traveler()
        {
            _inventory = new List<TravelerObject>();
            _locationsVisited = new List<MapLocation>();
        }

        public Traveler(string name, RaceType race, int spaceTimeLocationID) : base(name, race, spaceTimeLocationID)
        {
            _inventory = new List<TravelerObject>();
            _locationsVisited = new List<MapLocation>();
        }

        #endregion


        #region METHODS
        public override string Greeting()
        {
            return $"Hello, my name is {Name}, I am a {Race}, and I am from {_homePlanet}.";
        }


        #endregion
    }
}
