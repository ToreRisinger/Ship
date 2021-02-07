using Ship.Game.Event;
using Ship.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Ship.Game.GameObject;
using Ship.Game.Input;
using Game.Model;
using Ship.Network.Transport;
using System.Linq;

namespace Ship.Game
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;

        public ActionManager actionManager;

        private static int NO_ID = -1;
        private int thisPlayerId;

        private Dictionary<int, Player> players;
        private Dictionary<int, Character> characters;
        private Queue<GameState> gameStateQueue;

        private Player localPlayer;

        private int turnNumber;

        #region prefabs

        public UnityEngine.GameObject characterPrefab;

        #endregion

        private void Awake()
        {
            Log.debug("GameModelManager.Awake");
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Log.error("Instance already exists, destroying object!");
                Destroy(this);
            }

            players = new Dictionary<int, Player>();
            characters = new Dictionary<int, Character>();
            gameStateQueue = new Queue<GameState>();

            turnNumber = 0;

            thisPlayerId = NO_ID;
            registerToEvents();
        }

        private void registerToEvents()
        {
            EventManager.instance.AddListener(this, EEventType.PLAYER_JOINED_EVENT, OnPlayerJoined);
            EventManager.instance.AddListener(this, EEventType.CHARACTER_SPAWNED, OnCharacterSpawn);
        }

        void Start()
        {

        }

        void Update()
        {
            while (gameStateQueue.Count > 0)
            {
                GameState gameState = gameStateQueue.Dequeue();

                turnNumber = gameState.turnNumber;

                //TODO apply game state updates to game objects
            }
        }

        
        void FixedUpdate()
        {
            //Handle local player input
            if (thisPlayerId != NO_ID && players.ContainsKey(thisPlayerId))
            {
                Player localPlayer = players[thisPlayerId];
                Character character = localPlayer.character;

                float delta = Time.deltaTime;
                List<EPlayerAction> actions = actionManager.getActions().ToList();
                EDirection direction = character.direction;
                System.Numerics.Vector2 characterPosition = new System.Numerics.Vector2(character.transform.position.x, character.transform.position.y);

                //Send player command to server
                PlayerCommand cmdData = new PlayerCommand(turnNumber, delta, characterPosition, direction, actions);
                ConnectionManager.GetInstance().sendPlayerCommand(cmdData);
            }
        }

        public ActionManager getActionManager()
        {
            return actionManager;
        }

        public Player GetLocalPlayer()
        {
            return localPlayer;
        }

        #region events

        public void OnPlayerIdAssigned(int thisPlayerId)
        {
            this.thisPlayerId = thisPlayerId;
            Log.debug("[GameModelManager] player id assigned: " + thisPlayerId);
        }

        private void OnPlayerJoined(EventObject evnt)
        {
            PlayerJoinEvent playerJoinedEvent = (PlayerJoinEvent)evnt;
            Player player = new Player(playerJoinedEvent.playerId, playerJoinedEvent.username);
            players.Add(player.playerId, player);

            localPlayer = player;

            Log.debug("[GameModelManager] Player joined: id: " + player.playerId);
        }

        private void OnCharacterSpawn(EventObject evnt)
        {
            CharacterSpawnEvent characterSpawnEvent = (CharacterSpawnEvent)evnt;
            UnityEngine.GameObject gameObject = Instantiate(characterPrefab, new Vector2(characterSpawnEvent.position.X, characterSpawnEvent.position.Y), Quaternion.Euler(Vector3.forward));
            Character character = gameObject.GetComponent<Character>();
            character.init(characterSpawnEvent.owningPlayerId == thisPlayerId);
            characters.Add(characterSpawnEvent.gameObjectId, character);

            players[characterSpawnEvent.owningPlayerId].setCharacter(character);

            Log.debug("[GameModelManager] Character spawned: id: " + characterSpawnEvent.gameObjectId + ", parent player id: " + characterSpawnEvent.owningPlayerId);
        }

        public void OnNewGameState(GameState gameState)
        {
            gameStateQueue.Enqueue(gameState);
        }

        #endregion




    }
}

