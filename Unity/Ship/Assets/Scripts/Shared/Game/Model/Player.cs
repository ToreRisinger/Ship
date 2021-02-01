
using Ship.Network;
using Ship.Network.Transport;

namespace Ship.Game.Model
{
    public class Player : Transportable
    {
        public int playerId;
        public string username;

        public Player(int playerId, string username)
        {
            this.playerId = playerId;
            this.username = username;
        }

        public Player(Packet _packet) : base(_packet)
        {
            playerId = _packet.ReadInt();
            username = _packet.ReadString();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write(playerId);
            _packet.Write(username);
        }
    }
}
