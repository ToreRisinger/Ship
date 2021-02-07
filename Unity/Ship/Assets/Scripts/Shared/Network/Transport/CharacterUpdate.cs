using Game.Model;
using System.Numerics;

namespace Ship.Network.Transport
{
    public class CharacterUpdate : Transportable
    {
        public int id;
        public Vector2 position;
        public EDirection direction;

        public CharacterUpdate(int id, Vector2 position, EDirection direction) : base()
        {
            this.id = id;
            this.position = position;
            this.direction = direction;
        }

        public CharacterUpdate(Packet _packet)
        {
            id = _packet.ReadInt();
            position = _packet.ReadVector2();
            direction = (EDirection)_packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write(id);
            _packet.Write(position);
            _packet.Write((int)direction);
        }
    }
}
