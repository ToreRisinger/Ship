using Ship.Utilities;
using System.Numerics;

namespace Server.Game.Model
{
    public class GameObject
    {
        public int id;
        public Vector2 position;

        public GameObject(Vector2 position)
        {
            this.id = IdGenerator.getServerId();
            this.position = position;
        }
    }
}
