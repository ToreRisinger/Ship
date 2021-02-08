using Game.Model;
using System.Collections.Generic;
using System.Numerics;

namespace Ship.Network.Transport
{
    public class PlayerCommand : Transportable
    {
        public int turnNumber;
        public float deltaTime;
        public EDirection direction;
        public Vector2 position;
        public List<EPlayerAction> actions;
        public int playerId;

        public PlayerCommand(int playerId, int turnNumber, float deltaTime, Vector2 position, EDirection direction, List<EPlayerAction> actions) : base()
        {
            this.playerId = playerId;
            this.turnNumber = turnNumber;
            this.deltaTime = deltaTime;
            this.position = position;
            this.actions = actions;
            this.direction = direction;
        }

        public PlayerCommand(Packet packet)
        {
            playerId = packet.ReadInt();
            turnNumber = packet.ReadInt();
            deltaTime = packet.ReadFloat();
            position = packet.ReadVector2();
            direction = (EDirection)packet.ReadInt();
            int actionCount = packet.ReadInt();
            actions = new List<EPlayerAction>();
            for (int i = 0; i < actionCount; i++)
            {
                actions.Add((EPlayerAction)packet.ReadInt());
            }
        }

        public override void ToPacket(Packet packet)
        {
            packet.Write(playerId);
            packet.Write(turnNumber);
            packet.Write(deltaTime);
            packet.Write(new Vector2(position.X, position.Y));
            packet.Write((int)direction);
            packet.Write(actions.Count);
            foreach (EPlayerAction action in actions)
            {
                packet.Write((int)action);
            }
        }
    }
}
