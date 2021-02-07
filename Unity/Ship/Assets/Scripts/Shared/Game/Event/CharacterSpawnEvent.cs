using Ship.Network;
using Ship.Network.Transport;
using System.Numerics;

namespace Ship.Game.Event
{
    public class CharacterSpawnEvent : EventObject
    {
        public int owningPlayerId;
        public int gameObjectId;
        public Vector2 position;

        public CharacterSpawnEvent(int owningPlayerId, int gameObjectId, Vector2 position) : base(EEventType.CHARACTER_SPAWNED)
        {
            this.owningPlayerId = owningPlayerId;
            this.gameObjectId = gameObjectId;
            this.position = position;
        }

        public CharacterSpawnEvent(Packet packet) : base(EEventType.CHARACTER_SPAWNED)
        {
            owningPlayerId = packet.ReadInt();
            gameObjectId = packet.ReadInt();
            position = packet.ReadVector2();
        }

        public override void ToPacket(Packet packet)
        {
            base.ToPacket(packet);
            packet.Write(owningPlayerId);
            packet.Write(gameObjectId);
            packet.Write(position);
        }
    }
}
