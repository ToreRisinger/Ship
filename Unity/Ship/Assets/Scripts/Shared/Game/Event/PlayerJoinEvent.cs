using Ship.Game.Model;
using Ship.Network;

namespace Ship.Game.Event
{
    public class PlayerJoinEvent : EventObject
    {
        public PlayerTp player; 

        public PlayerJoinEvent(PlayerTp player) : base(EEventType.PLAYER_JOINED_EVENT)
        {
            this.player = player;
        }

        public PlayerJoinEvent(Packet _packet) : base(EEventType.PLAYER_JOINED_EVENT)
        {
            player = new PlayerTp(_packet);
        }

        public override void ToPacket(Packet _packet)
        {
            base.ToPacket(_packet);
            player.ToPacket(_packet);
        }
    }
}
