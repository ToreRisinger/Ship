using Ship.Game.Event;
using Ship.Game.Model;
using Ship.Server.Network;
using System.Collections.Generic;

namespace Server.Game
{
    public class GameManager
    {

        private ConnectionManager connectionManager;
        private Dictionary<int, Player> players;
        private Queue<EventObject> events;
        public Dictionary<int, GameState> gameStates;
        private int turnNumber;


        //todo map

        /* 
         * Each map section has its own GameState and events
         * Clients are sent the relevant map section gamestates as a combined map state
         * 
         */

        public GameManager()
        {

        }

        public void init(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            players = new Dictionary<int, Player>();
            events = new Queue<EventObject>();
            gameStates = new Dictionary<int, GameState>();
            turnNumber = 0;
        }

        public void update()
        {


            //Create new game state
            GameState gameState = new GameState(turnNumber, events);
            gameStates.Add(turnNumber, gameState);

            removeOldGameStates();

            sendGameState(gameState);

            events.Clear();
            turnNumber++;
        }

        #region events

        public void OnClientJoin(int clientId, string username)
        {
            Player player = new Player(clientId, username);
            PlayerJoinEvent evnt = new PlayerJoinEvent(player);
            pushEvent(evnt);
            players.Add(player.playerId, player);
        }

        public void OnClientLeave(int clientId)
        {
            if(players.ContainsKey(clientId))
            {
                Player player = players[clientId];
                PlayerLeftEvent evnt = new PlayerLeftEvent(player);
                pushEvent(evnt);
                players.Remove(clientId);
            }
        }

        #endregion

        private void pushEvent(EventObject evnt) 
        {
            events.Enqueue(evnt);
        }

        private void removeOldGameStates()
        {
            if(gameStates.Count > 10)
            {
                int tmpTurnNumber = turnNumber - 10;
                while(tmpTurnNumber > 0)
                {
                    if(gameStates.ContainsKey(tmpTurnNumber))
                    {
                        gameStates.Remove(tmpTurnNumber);
                    } 
                    else
                    {
                        break;
                    }
                    tmpTurnNumber--;
                }
            }
        }

        private void sendGameState(GameState gameState)
        {
            connectionManager.OnSendGameState(gameState);
        }
    }
}
