﻿using Ship.Game.Event;
using System;
using System.Collections.Generic;

namespace Ship.Network.Transport
{
    public class GameState : Transportable
    {
        public int turnNumber;
        public Queue<EventObject> events;
        public List<CharacterUpdate> characterUpdates;

        public GameState(int turnNumber, Queue<EventObject> events, List<CharacterUpdate> characterUpdates)
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
                events.Enqueue(ReadEventFromPacket(packet));
            }

            characterUpdates = new List<CharacterUpdate>();
            int characterUpdateCount = packet.ReadInt();
            for (int i = 0; i < characterUpdateCount; i++)
            {
                characterUpdates.Add(new CharacterUpdate(packet));
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