
using System.Collections.Generic;

namespace Server.Game.Model
{
    public class Sector
    {

        private Dictionary<int, GameObject> gameObjects;

        public Sector()
        {
            gameObjects = new Dictionary<int, GameObject>();
        }

        public void addGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject.id, gameObject);
        }
    }
}
