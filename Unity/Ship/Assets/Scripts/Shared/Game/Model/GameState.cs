using Ship.Game.Event;
using Ship.Network;
using Ship.Network.Transport;
using System;
using System.Collections.Generic;

namespace Ship.Game.Model
{
    public class GameState : Transportable
    {
        public int turnNumber;
        public Queue<EventObject> events;

        public GameState(int turnNumber, Queue<EventObject> events)
        {
            this.turnNumber = turnNumber;
            this.events = events;
        }

        public GameState(Packet packet) : base(packet)
        {
            events = new Queue<EventObject>();
            turnNumber = packet.ReadInt();
            int eventCount = packet.ReadInt();
            for (int i = 0; i < eventCount; i++)
            {
                events.Enqueue(ReadEventFromPacket(packet));
            }
        }

        public override void ToPacket(Packet packet)
        {
            packet.Write(turnNumber);

            packet.Write(events.Count);
            Queue<EventObject> tmpEvents = new Queue<EventObject>(events);
            for (int i = 0; i < events.Count; i++)
            {
                EventObject evnt = tmpEvents.Dequeue();
                packet.Write((int)evnt.GetEventType());
                evnt.ToPacket(packet);
            }
        }

        private static Dictionary<int, Func<Packet, EventObject>> eventMap = new Dictionary<int, Func<Packet, EventObject>>
        {
            {(int)EEventType.PLAYER_JOINED_EVENT, (packet) => { return new PlayerJoinEvent(packet); } },
            {(int)EEventType.PLAYER_LEFT_EVENT, (packet) => { return new PlayerLeftEvent(packet); } },
            {(int)EEventType.CHARACTER_SPAWNED, (packet) => { return new CharacterSpawnEvent(packet); } },
        };

        private EventObject ReadEventFromPacket(Packet _packet)
        {
            int eventId = _packet.ReadInt();
            return eventMap[eventId](_packet);
        }
    }
}
