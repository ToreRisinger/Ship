using Ship.Game.Model;
using System.Collections.Generic;

namespace Server.Game
{
    public class GameManager
    {

        private Dictionary<int, Player> players;

        public GameManager()
        {

        }

        public void init()
        {
            players = new Dictionary<int, Player>();
        }

        public void update()
        {

        }

        /*
         *  EVENTS
         */

        public void OnClientJoin(int clientId, string username)
        {
            Player player = new Player(clientId, username);
            players.Add(player.GetId(), player);
            //add player join event
        }

        public void OnClientLeave(int clientId)
        {
            if(players.ContainsKey(clientId))
            {
                players.Remove(clientId);
                //add player left event
            }
        }
    }
}
