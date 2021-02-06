using Ship.Game.Event;
using Ship.Game.Model;
using Ship.Server.Network;
using Ship.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace Server.Game
{
    public class GameManager
    {

        private ConnectionManager connectionManager;

        private Dictionary<int, PlayerTp> players;
        private Dictionary<int, CharacterTp> characters;

        private Queue<EventObject> events;
        public Dictionary<int, GameStateTp> gameStates;
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
            players = new Dictionary<int, PlayerTp>();
            characters = new Dictionary<int, CharacterTp>();
            events = new Queue<EventObject>();
            gameStates = new Dictionary<int, GameStateTp>();
            turnNumber = 0;
        }

        public void update()
        {


            //Create new game state
            GameStateTp gameState = new GameStateTp(turnNumber, events);
            gameStates.Add(turnNumber, gameState);

            removeOldGameStates();

            sendGameState(gameState);

            events.Clear();
            turnNumber++;
        }

        #region events

        public void OnClientJoin(int clientId, string username)
        {
            PlayerTp player = new PlayerTp(clientId, username);
            PlayerJoinEvent playerJoinEvent = new PlayerJoinEvent(player);
            pushEvent(playerJoinEvent);

            CharacterTp character = new CharacterTp(player.playerId, IdGenerator.getServerId(), new Vector2(0, 0));
            CharacterSpawnEvent characterSpawnEvent = new CharacterSpawnEvent(character);
            pushEvent(characterSpawnEvent);

            players.Add(player.playerId, player);
            characters.Add(character.id, character);
        }

        public void OnClientLeave(int clientId)
        {
            if(players.ContainsKey(clientId))
            {
                PlayerTp player = players[clientId];
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

        private void sendGameState(GameStateTp gameState)
        {
            connectionManager.OnSendGameState(gameState);
        }
    }
}
