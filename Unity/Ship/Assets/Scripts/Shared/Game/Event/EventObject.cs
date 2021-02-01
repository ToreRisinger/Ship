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
            eventType = (EEventType)_packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write((int)eventType);
        }

        public EEventType GetEventType()
        {
            return eventType;
        }
    }
}
