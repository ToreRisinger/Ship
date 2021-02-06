using Ship.Game.Model;
using Ship.Network;

namespace Ship.Game.Event
{
    public class PlayerLeftEvent : EventObject
    {
        public PlayerTp player; 

        public PlayerLeftEvent(PlayerTp player) : base(EEventType.PLAYER_LEFT_EVENT)
        {
            this.player = player;
        }

        public PlayerLeftEvent(Packet _packet) : base(_packet)
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
