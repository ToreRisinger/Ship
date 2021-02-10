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

        public PlayerCommand(int playerId, int _turnNumber, float _deltaTime, Vector2 _position, EDirection _direction, List<EPlayerAction> _actions) : base()
        {
            this.playerId = playerId;
            turnNumber = _turnNumber;
            deltaTime = _deltaTime;
            position = _position;
            actions = _actions;
            direction = _direction;
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
