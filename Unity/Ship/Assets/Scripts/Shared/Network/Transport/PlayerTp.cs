
using Ship.Network;
using Ship.Network.Transport;

namespace Ship.Game.Model
{
    public class PlayerTp : Transportable
    {
        public int playerId;
        public string username;

        public PlayerTp(int playerId, string username)
        {
            this.playerId = playerId;
            this.username = username;
        }

        public PlayerTp(Packet _packet) : base(_packet)
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
