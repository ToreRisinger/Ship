namespace Server.Game.Model
{
    public class Player
    {
        public int playerId;
        public string username;
        public Character character;

        public Player(int playerId, string username, Character character)
        {
            this.playerId = playerId;
            this.username = username;
            this.character = character;
        }
    }
}
