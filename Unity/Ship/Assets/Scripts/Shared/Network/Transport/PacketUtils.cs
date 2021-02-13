using Ship.Game.Event;
using System;
using System.Collections.Generic;

namespace Ship.Network.Transport
{
    public class PacketUtils
    {

        private PacketUtils()
        {

        }

        private static Dictionary<int, Func<Packet, EventObject>> eventMap = new Dictionary<int, Func<Packet, EventObject>>
        {
            {(int)EEventType.PLAYER_JOINED_EVENT, (packet) => { return new PlayerJoinEvent(packet); } },
            {(int)EEventType.PLAYER_LEFT_EVENT, (packet) => { return new PlayerLeftEvent(packet); } },

            {(int)EEventType.CHARACTER_SPAWNED, (packet) => { return new CharacterSpawnEvent(packet); } },
            {(int)EEventType.TREE_SPAWNED, (packet) => { return new TreeSpawnEvent(packet); } },

        };

        public static EventObject ReadEventFromPacket(Packet _packet)
        {
            int eventId = _packet.ReadInt();
            return eventMap[eventId](_packet);
        }
    }
}
