using Game.Model;
using System.Numerics;

namespace Server.Game.Model
{
    public class Character : GameObject
    {
        public int owningPlayerId;

        public EDirection direction;

        public Character(int id, Vector2 position, int owningPlayerId) : base(id, position)
        {
            this.owningPlayerId = owningPlayerId;
            this.direction = EDirection.DOWN;
        }
    }
}
