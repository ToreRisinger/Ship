using Game.Model;
using Server.Game.Model;
using Ship.Game.Event;
using Ship.Network.Transport;
using Ship.Server.Network;
using Ship.Utilities;
using System.Collections.Generic;
using System.Numerics;

namespace Server.Game
{
    public class GameManager
    {

        private ConnectionManager connectionManager;

        public Dictionary<int, Player> players;
        public Dictionary<int, Character> characters;
        //public Map map;
        public int MAP_SIZE;

        private Queue<EventObject> events;
        private Dictionary<int, GameState> gameStates;
        private Queue<PlayerCommand> playerCommands;

        private int turnNumber;

        public GameManager()
        {

        }

        public void init(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;

            players = new Dictionary<int, Player>();
            characters = new Dictionary<int, Character>();
            //map = new Map(MAP_SIZE);

            events = new Queue<EventObject>();
            gameStates = new Dictionary<int, GameState>();
            playerCommands = new Queue<PlayerCommand>();

            turnNumber = 0;
        }

        public void update()
        {
            handlePlayerCommands();

            handleGameStates();

            turnNumber++;
        }

        #region private methods

        private void handlePlayerCommands()
        {
            while (playerCommands.Count > 0)
            {
                PlayerCommand playerCommand = playerCommands.Dequeue();

                if (players.ContainsKey(playerCommand.playerId) && players[playerCommand.playerId].character != null)
                {
                    Player player = players[playerCommand.playerId];
                    Character character = player.character;
                    character.position = playerCommand.position;
                    character.direction = playerCommand.direction;
                    character.isRunning = playerCommand.actions.Contains(EPlayerAction.DOWN) || playerCommand.actions.Contains(EPlayerAction.RIGHT) || playerCommand.actions.Contains(EPlayerAction.LEFT) || playerCommand.actions.Contains(EPlayerAction.UP);
                }
            }
        }

        private void handleGameStates()
        {
            List<CharacterPositionUpdate> characterUpdates = new List<CharacterPositionUpdate>();
            foreach (Character character in characters.Values)
            {
                characterUpdates.Add(new CharacterPositionUpdate(character.id, character.position, character.direction, character.isRunning));
            }

            GameState gameState = new GameState(turnNumber, events, characterUpdates);
            gameStates.Add(turnNumber, gameState);

            removeOldGameStates();

            sendGameState(gameState);

            events.Clear();
        }

        private void pushEvent(EventObject evnt)
        {
            events.Enqueue(evnt);
        }

        private void removeOldGameStates()
        {
            if (gameStates.Count > 10)
            {
                int tmpTurnNumber = turnNumber - 10;
                while (tmpTurnNumber > 0)
                {
                    if (gameStates.ContainsKey(tmpTurnNumber))
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

        #endregion

        #region events

        public void OnPlayerJoin(int clientId, string username)
        {
            Player player = new Player(clientId, username, null);

            PlayerJoinEvent playerJoinEvent = new PlayerJoinEvent(clientId, username);
            pushEvent(playerJoinEvent);

            players.Add(player.playerId, player);

            Log.debug("Player joined. Players on server: " + players.Count);
        }

        public void OnPlayerFinishLoading(int clientId)
        {
            Character character = new Character(new Vector2(0, 0), clientId);
            CharacterSpawnEvent characterSpawnEvent = new CharacterSpawnEvent(clientId, character.id, new Vector2(0, 0));
            players[clientId].character = character;
            pushEvent(characterSpawnEvent);
            characters.Add(character.id, character);

            Log.debug("Player finished loading. Spawning character.");
        }

        public void OnClientLeave(int clientId)
        {
            if(players.ContainsKey(clientId))
            {
                PlayerLeftEvent evnt = new PlayerLeftEvent(clientId);
                pushEvent(evnt);
                characters.Remove(players[clientId].character.id);
                players.Remove(clientId);

                Log.debug("Client left. Players on server: " + players.Count);
            }
        }

        public void onPlayerCommandReceived(PlayerCommand playerCommand)
        {
            playerCommands.Enqueue(playerCommand);
        }

        #endregion


    }
}
