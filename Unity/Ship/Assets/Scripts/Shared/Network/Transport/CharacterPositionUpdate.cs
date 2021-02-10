using Game.Model;
using System.Numerics;

namespace Ship.Network.Transport
{
    public class CharacterPositionUpdate : Transportable
    {
        public int id;
        public Vector2 position;
        public EDirection direction;
        public bool isRunning;

        public CharacterPositionUpdate(int id, Vector2 position, EDirection direction, bool isRunning) : base()
        {
            this.id = id;
            this.position = position;
            this.direction = direction;
            this.isRunning = isRunning;
        }

        public CharacterPositionUpdate(Packet _packet)
        {
            id = _packet.ReadInt();
            position = _packet.ReadVector2();
            isRunning = _packet.ReadBool();
            direction = (EDirection)_packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write(id);
            _packet.Write(position);
            _packet.Write(isRunning);
            _packet.Write((int)direction);
        }
    }
}
