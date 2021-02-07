using Ship.Network;
using Ship.Network.Transport;

namespace Ship.Game.Event
{
    public abstract class EventObject : Transportable
    {

        private EEventType eventType;

        public EventObject(EEventType eventType)
        {
            this.eventType = eventType;
        }

        public EventObject(Packet _packet)
        {
            
        }

        public override void ToPacket(Packet _packet)
        {
           
        }

        public EEventType GetEventType()
        {
            return eventType;
        }
    }
}
