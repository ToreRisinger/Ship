
using Ship.Network;
using System.Numerics;

namespace Ship.Game.Event
{
    public class TreeSpawnEvent : EventObject
    {
        public int gameObjectId;
        public Vector2 position;

        public TreeSpawnEvent(int gameObjectId, Vector2 position) : base(EEventType.TREE_SPAWNED)
        {
            this.gameObjectId = gameObjectId;
            this.position = position;
        }

        public TreeSpawnEvent(Packet packet) : base(EEventType.TREE_SPAWNED)
        {
            gameObjectId = packet.ReadInt();
            position = packet.ReadVector2();
        }

        public override void ToPacket(Packet packet)
        {
            base.ToPacket(packet);
            packet.Write(gameObjectId);
            packet.Write(position);
        }
    }
}
