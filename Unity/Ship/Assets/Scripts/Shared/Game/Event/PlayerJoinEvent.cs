using Ship.Network;

namespace Ship.Game.Event
{
    public class PlayerJoinEvent : EventObject
    {
        public int playerId;
        public string username;

        public PlayerJoinEvent(int playerId, string username) : base(EEventType.PLAYER_JOINED_EVENT)
        {
            this.playerId = playerId;
            this.username = username;
        }

        public PlayerJoinEvent(Packet _packet) : base(EEventType.PLAYER_JOINED_EVENT)
        {
            playerId = _packet.ReadInt();
            username = _packet.ReadString();
        }

        public override void ToPacket(Packet _packet)
        {
            base.ToPacket(_packet);
            _packet.Write(playerId);
            _packet.Write(username);
        }
    }
}
