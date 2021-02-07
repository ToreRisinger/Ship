using System.Numerics;

namespace Server.Game.Model
{
    public class GameObject
    {
        public int id;
        public Vector2 position;

        public GameObject(int id, Vector2 position)
        {
            this.id = id;
            this.position = position;
        }
    }
}
