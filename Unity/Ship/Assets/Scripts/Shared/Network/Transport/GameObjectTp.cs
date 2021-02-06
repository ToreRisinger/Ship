using Ship.Network;
using Ship.Network.Transport;
using System.Numerics;

namespace Ship.Game.Model
{
    public class GameObjectTp : Transportable
    {
        public int id;
        public Vector2 position;

        public GameObjectTp(int id, Vector2 position) : base()
        {
            this.id = id;
            this.position = position;
        }

        public GameObjectTp(Packet _packet) : base(_packet)
        {
            id = _packet.ReadInt();
            position = _packet.ReadVector2();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write(id);
            _packet.Write(position);
        }


    }
}
