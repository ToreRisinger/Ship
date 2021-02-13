using Ship.Game.Event;
using System;
using System.Collections.Generic;

namespace Ship.Network.Transport
{
    public class GameState : Transportable
    {
        public int turnNumber;
        public Queue<EventObject> events;
        public List<CharacterPositionUpdate> characterUpdates;

        public GameState(int turnNumber, Queue<EventObject> events, List<CharacterPositionUpdate> characterUpdates)
        {
            this.turnNumber = turnNumber;
            this.events = events;
            this.characterUpdates = characterUpdates;
        }

        public GameState(Packet packet) : base(packet)
        {
            events = new Queue<EventObject>();
            turnNumber = packet.ReadInt();
            int eventCount = packet.ReadInt();
            for (int i = 0; i < eventCount; i++)
            {
                events.Enqueue(PacketUtils.ReadEventFromPacket(packet));
            }

            characterUpdates = new List<CharacterPositionUpdate>();
            int characterUpdateCount = packet.ReadInt();
            for (int i = 0; i < characterUpdateCount; i++)
            {
                characterUpdates.Add(new CharacterPositionUpdate(packet));
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

            packet.Write(characterUpdates.Count);
            for (int i = 0; i < characterUpdates.Count; i++)
            {
                characterUpdates[i].ToPacket(packet);
            }
        }
    }
}
