using Ship.Network;

namespace Ship.Game.Event
{
    public class PlayerLeftEvent : EventObject
    {
        public int playerId;

        public PlayerLeftEvent(int playerId) : base(EEventType.PLAYER_LEFT_EVENT)
        {
            this.playerId = playerId;
        }

        public PlayerLeftEvent(Packet _packet) : base(_packet)
        {
            playerId = _packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            base.ToPacket(_packet);
            _packet.Write(playerId);
        }
    }
}
