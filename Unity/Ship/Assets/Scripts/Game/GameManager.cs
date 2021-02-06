using Ship.Game.Event;
using Ship.Game.Model;
using Ship.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Ship.Game.GameObject;
using Ship.Game.Input;

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

        private Player localPlayer;

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

        }

        void FixedUpdate()
        {
            //Handle local player input
            if (thisPlayerId != NO_ID && players.ContainsKey(thisPlayerId))
            {
                Player localPlayer = players[thisPlayerId];
                Character character = localPlayer.character;

                float delta = Time.deltaTime;
                HashSet<EPlayerAction> actions = actionManager.getActions();

                //players[thisPlayerId].character.Move(actions, delta);

                //MoveLocalPlayer(character, actions, delta);

                /*
                character.SetDirection(actions);
                EObjectDirection direction = character.direction;
                System.Numerics.Vector2 characterPosition = new System.Numerics.Vector2(character.transform.position.x, character.transform.position.y);

                //Send player command to server
                PlayerCommandData cmdData = new PlayerCommandData(turnNumber, delta, characterPosition, direction, actions, abilityActivations);
                ClientSend.PlayerCommand(cmdData);
                */
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
            PlayerTp playerTp = playerJoinedEvent.player;
            Player player = new Player(playerTp.playerId, playerTp.username);
            players.Add(playerTp.playerId, player);

            localPlayer = player;

            Log.debug("[GameModelManager] Player joined: id: " + playerTp.playerId);
        }

        private void OnCharacterSpawn(EventObject evnt)
        {
            CharacterSpawnEvent characterSpawnEvent = (CharacterSpawnEvent)evnt;
            CharacterTp characterTp = characterSpawnEvent.character;
            UnityEngine.GameObject gameObject = Instantiate(characterPrefab, new Vector2(characterTp.position.X, characterTp.position.Y), Quaternion.Euler(Vector3.forward));
            Character character = gameObject.GetComponent<Character>();
            character.init(characterTp, characterTp.owningPlayerId == thisPlayerId);
            characters.Add(characterTp.id, character);

            players[characterTp.owningPlayerId].setCharacter(character);

            Log.debug("[GameModelManager] Character spawned: id: " + characterTp.id + ", parent player id: " + characterTp.owningPlayerId);
        }

        #endregion




    }
}

