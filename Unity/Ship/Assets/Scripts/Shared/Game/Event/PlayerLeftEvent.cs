using Ship.Game.Model;
using Ship.Network;

namespace Ship.Game.Event
{
    public class PlayerLeftEvent : EventObject
    {
        public Player player; 

        public PlayerLeftEvent(Player player) : base(EEventType.PLAYER_LEFT_EVENT)
        {
            this.player = player;
        }

        public PlayerLeftEvent(Packet _packet) : base(_packet)
        {
            player = new Player(_packet);
        }

        public override void ToPacket(Packet _packet)
        {
            base.ToPacket(_packet);
            player.ToPacket(_packet);
        }
    }
}
