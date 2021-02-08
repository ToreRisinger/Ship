using Ship.Game.GameObject;

namespace Ship.Game 
{
    public class Player
    {
        public int playerId;
        public string username;
        public CharacterBase character;

        public Player(int playerId, string username)
        {
            this.playerId = playerId;
            this.username = username;
        }

        public void setCharacter(CharacterBase character)
        {
            this.character = character;
        }

        
    }
}
