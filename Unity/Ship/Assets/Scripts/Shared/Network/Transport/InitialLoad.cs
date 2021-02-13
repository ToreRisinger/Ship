using Ship.Game.Event;
using System.Collections.Generic;

namespace Ship.Network.Transport
{
    public class InitialLoad : Transportable
    {
        public List<PlayerJoinEvent> players;
        public List<CharacterSpawnEvent> characters;
        public int numberOfSectors;

        public InitialLoad(List<PlayerJoinEvent> players, List<CharacterSpawnEvent> characters, int numberOfSectors)
        {
            this.players = players;
            this.characters = characters;
            this.numberOfSectors = numberOfSectors;
        }

        public InitialLoad(Packet packet) : base()
        {
            players = new List<PlayerJoinEvent>();
            int playerCount = packet.ReadInt();
            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new PlayerJoinEvent(packet));
            }

            characters = new List<CharacterSpawnEvent>();
            int characterCount = packet.ReadInt();
            for (int i = 0; i < characterCount; i++)
            {
                characters.Add(new CharacterSpawnEvent(packet));
            }

            numberOfSectors = packet.ReadInt();
        }

        public override void ToPacket(Packet packet)
        {
            packet.Write(players.Count);
            for (int i = 0; i < players.Count; i++)
            {
                players[i].ToPacket(packet);
            }

            packet.Write(characters.Count);
            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].ToPacket(packet);
            }

            packet.Write(numberOfSectors);
        }
    }
}
