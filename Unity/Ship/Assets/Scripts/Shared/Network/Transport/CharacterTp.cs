

using Ship.Network;
using System.Numerics;

namespace Ship.Game.Model
{
    public class CharacterTp : GameObjectTp
    {
        public int owningPlayerId;

        public CharacterTp(int owningPlayerId, int gameObjectId, Vector2 position) : base(gameObjectId, position)
        {
            this.owningPlayerId = owningPlayerId;
        }

        public CharacterTp(Packet _packet) : base(_packet)
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
