using Ship.Game.Event;
using Ship.Game.Model;
using Ship.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game
{
    public class GameModelManager : MonoBehaviour
    {

        public static GameModelManager instance;

        private int thisPlayerId;

        private Dictionary<int, Player> players;
        private Dictionary<int, Character> characters;

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
            thisPlayerId = -1;
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

        #region events

        public void OnPlayerIdAssigned(int thisPlayerId)
        {
            this.thisPlayerId = thisPlayerId;
            Log.debug("[GameModelManager] player id assigned: " + thisPlayerId);
        }

        private void OnPlayerJoined(EventObject evnt)
        {
            PlayerJoinEvent playerJoinedEvent = (PlayerJoinEvent)evnt;
            Player player = playerJoinedEvent.player;
            players.Add(player.playerId, player);

            Log.debug("[GameModelManager] Player joined: id: " + player.playerId);
        }

        private void OnCharacterSpawn(EventObject evnt)
        {
            CharacterSpawnEvent characterSpawnEvent = (CharacterSpawnEvent)evnt;
            Character character = characterSpawnEvent.character;
            characters.Add(character.id, character);

            Log.debug("[GameModelManager] Character spawned: id: " + character.id + ", parent player id: " + character.owningPlayerId);
        }

        #endregion




    }
}

