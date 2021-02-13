
using Ship.Game.Event;
using System.Collections.Generic;

namespace Ship.Network.Transport
{
    public class MapLoad : Transportable
    {
        public int x;
        public int y;

        public List<EventObject> events;


        public MapLoad(int x, int y, List<EventObject> events)
        {
            this.x = x;
            this.y = y;
            this.events = events;
        }

        public MapLoad(Packet packet) : base(packet)
        {
            x = packet.ReadInt();
            y = packet.ReadInt();
            events = new List<EventObject>();
            int eventCount = packet.ReadInt();
            for (int i = 0; i < eventCount; i++)
            {
                events.Add(PacketUtils.ReadEventFromPacket(packet));
            }
        }

        public override void ToPacket(Packet packet)
        {
            packet.Write(x);
            packet.Write(y);

            packet.Write(events.Count);
            for (int i = 0; i < events.Count; i++)
            {
                EventObject evnt = events[i];
                packet.Write((int)evnt.GetEventType());
                evnt.ToPacket(packet);
            }
        }
    }
}
