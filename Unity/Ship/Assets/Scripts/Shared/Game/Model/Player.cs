
namespace Ship.Game.Model
{
    public class Player
    {
        private int playerId;
        private string username;

        public Player(int playerId, string username)
        {
            this.playerId = playerId;
            this.username = username;
        }

        public int GetPlayerId()
        {
            return playerId;
        }

        public string GetUsername()
        {
            return username;
        }
    }
}
