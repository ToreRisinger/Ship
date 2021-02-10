using Game.Model;
using System.Numerics;

namespace Server.Game.Model
{
    public class Character : GameObject
    {
        public int owningPlayerId;

        public EDirection direction;
        public bool isRunning;

        public Character(int id, Vector2 position, int owningPlayerId) : base(id, position)
        {
            this.owningPlayerId = owningPlayerId;
            direction = EDirection.DOWN;
            isRunning = false;
        }
    }
}
