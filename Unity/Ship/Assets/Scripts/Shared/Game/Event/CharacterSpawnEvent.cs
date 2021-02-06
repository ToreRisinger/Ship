using Ship.Game.Model;
using Ship.Network;

namespace Ship.Game.Event
{
    public class CharacterSpawnEvent : EventObject
    {
        public CharacterTp character;

        public CharacterSpawnEvent(CharacterTp character) : base(EEventType.CHARACTER_SPAWNED)
        {
            this.character = character;
        }

        public CharacterSpawnEvent(Packet _packet) : base(EEventType.CHARACTER_SPAWNED)
        {
            character = new CharacterTp(_packet);
        }

        public override void ToPacket(Packet _packet)
        {
            base.ToPacket(_packet);
            character.ToPacket(_packet);
        }
    }
}
