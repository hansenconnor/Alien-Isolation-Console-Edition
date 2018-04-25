using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public class Universe
    {
        #region ***** define all lists to be maintained by the Universe object *****

        //
        // list of all space-time locations and game objects
        //
      
        private List<GameObject> _gameObjects;
        private List<NPC> _npcs;

        public List<NPC> Npcs
        {
            get { return _npcs; }
            set { _npcs = value; }
        }

        public List<GameObject> GameObjects
        {
            get { return _gameObjects; }
            set { _gameObjects = value; }
        }

        #endregion

        #region ***** constructor *****

        //
        // default Universe constructor
        //
        public Universe()
        {
            //
            // add all of the universe objects to the game
            // 
            IntializeUniverse();
        }

        #endregion

        private void IntializeUniverse()
        {
            _gameObjects = UniverseObjects.gameObjects;
            _npcs = UniverseObjects.Npcs;
        }
    }
}
