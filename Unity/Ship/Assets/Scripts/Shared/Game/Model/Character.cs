

using Ship.Network;
using System.Numerics;

namespace Ship.Game.Model
{
    public class Character : GameObject
    {
        public int owningPlayerId;

        public Character(int owningPlayerId, int gameObjectId, Vector2 position) : base(gameObjectId, position)
        {
            this.owningPlayerId = owningPlayerId;
        }

        public Character(Packet _packet) : base(_packet)
        {
            owningPlayerId = _packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            base.ToPacket(_packet);
            _packet.Write(owningPlayerId);
        }
    }
}
