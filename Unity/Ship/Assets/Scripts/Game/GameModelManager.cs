using Ship.Game.Event;
using Ship.Game.Model;
using Ship.Shared.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game
{
    public class GameModelManager : MonoBehaviour
    {

        public static GameModelManager instance;

        private int thisPlayerId;
        private Dictionary<int, Player> players;

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
            thisPlayerId = -1;
            registerToEvents();
        }

        void Start()
        {

        }

        void Update()
        {

        }

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

            Log.debug("[GameModelManager] Player joined: id: " + playerJoinedEvent.player.playerId);
        }

        private void registerToEvents()
        {
            EventManager.instance.AddListener(this, EEventType.PLAYER_JOINED_EVENT, OnPlayerJoined);
        }
    }
}

