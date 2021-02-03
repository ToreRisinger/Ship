using Ship.Game.Model;
using Ship.Network;

namespace Ship.Game.Event
{
    public class CharacterSpawnEvent : EventObject
    {
        public Character character;

        public CharacterSpawnEvent(Character character) : base(EEventType.CHARACTER_SPAWNED)
        {
            this.character = character;
        }

        public CharacterSpawnEvent(Packet _packet) : base(EEventType.CHARACTER_SPAWNED)
        {
            character = new Character(_packet);
        }

        public override void ToPacket(Packet _packet)
        {
            base.ToPacket(_packet);
            character.ToPacket(_packet);
        }
    }
}
