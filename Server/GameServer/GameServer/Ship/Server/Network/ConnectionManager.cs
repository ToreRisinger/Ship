﻿using Server.Game;
using Server.Game.Model;
using Ship.Game.Event;
using Ship.Network;
using Ship.Network.Transport;
using System.Collections.Generic;

namespace Ship.Server.Network
{
    public class ConnectionManager
    {
        private ClientManager clientManager;
        private GameManager gameManager;

        private Dictionary<int, int> playerMapLoading;

        public ConnectionManager() 
        {
            playerMapLoading = new Dictionary<int, int>();
        }

        public void init(ClientManager clientHandler, GameManager gameManager)
        {
            this.clientManager = clientHandler;
            this.gameManager = gameManager;
        }

        public void update()
        {
            foreach(var client in playerMapLoading)
            {
                //TODO
            }
        }

        #region send

        private void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            foreach (var client in clientManager.GetClients().Values) {
                client.tcp.SendData(_packet);
            }
        }

        private void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            foreach (var client in clientManager.GetClients().Values)
            {
                if(client.id != _exceptClient)
                {
                    client.tcp.SendData(_packet);
                }
            }
        }

        private void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            clientManager.GetClients()[_toClient].tcp.SendData(_packet);
        }

        private void SendUDPDataToAll(Packet _packet)
        {
            foreach (var client in clientManager.GetClients().Values)
            {
                client.udp.SendData(_packet);
            }
        }

        private void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            foreach (var client in clientManager.GetClients().Values)
            {
                if (client.id != _exceptClient)
                {
                    client.udp.SendData(_packet);
                }
            }
        }

        private void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            clientManager.GetClients()[_toClient].udp.SendData(_packet);
        }

        #endregion


        #region RX
        public void OnClientConnected(int clientId)
        {
            using (Packet _packet = new Packet((int)PacketTypes.ServerPackets.ASSIGN_CLIENT_ID))
            {
                ClientId clientIdObj = new ClientId(clientId);
                clientIdObj.ToPacket(_packet);
                     
                SendTCPData(clientId, _packet);
            }
        }

        public void OnClientDisconnected(int clientId)
        {
            gameManager.OnClientLeave(clientId);
            if(playerMapLoading.ContainsKey(clientId))
            {
                playerMapLoading.Remove(clientId);
            }
        }

        public void OnClientIdReceived(int fromClient, ClientId clientId)
        {
            if(fromClient != clientId.clientId)
            {
                OnServerErrorResponse(fromClient, EServerErrorCode.WRONG_CLIENT_ID);
                if(clientManager.GetClients().ContainsKey(fromClient))
                {
                    clientManager.GetClients()[fromClient].Disconnect();
                }
            } else
            {
                sendInitialLoad(fromClient);
                gameManager.OnPlayerJoin(fromClient, "username");
                gameManager.OnPlayerFinishLoading(fromClient);
                //playerMapLoading.Add(fromClient, gameManager.MAP_SIZE);
            }
        }

        public void OnPlayerCommandReceived(PlayerCommand playerCommand)
        {
            gameManager.onPlayerCommandReceived(playerCommand);
        }

        #endregion


        #region TX

        private void OnServerErrorResponse(int toClient, EServerErrorCode errorCode)
        {
            using (Packet _packet = new Packet((int)PacketTypes.ServerPackets.SERVER_ERROR))
            {
                ServerError serverErrorObj = new ServerError(errorCode);
                serverErrorObj.ToPacket(_packet);

                SendTCPData(toClient, _packet);
            }
        }

        public void OnSendGameState(GameState gameState)
        {
            using (Packet _packet = new Packet((int)PacketTypes.ServerPackets.GAME_STATE))
            {
                gameState.ToPacket(_packet);
                SendTCPDataToAll(_packet);
            }
        }

        #endregion

        private void sendInitialLoad(int toClient)
        {
            Dictionary<int, Player> players = gameManager.players;
            Dictionary<int, Character> characters = gameManager.characters;

            List<PlayerJoinEvent> playerJoinEvents = new List<PlayerJoinEvent>();
            foreach (Player player in players.Values)
            {
                playerJoinEvents.Add(new PlayerJoinEvent(player.playerId, player.username));
            }

            List<CharacterSpawnEvent> spawnCharacterEvents = new List<CharacterSpawnEvent>();
            foreach (Character character in characters.Values)
            {
                spawnCharacterEvents.Add(new CharacterSpawnEvent(character.owningPlayerId, character.id, character.position));
            }

            InitialLoad initialLoad = new InitialLoad(playerJoinEvents, spawnCharacterEvents, gameManager.MAP_SIZE);

            using (Packet _packet = new Packet((int)PacketTypes.ServerPackets.INITIAL_LOAD))
            {
                initialLoad.ToPacket(_packet);
                SendTCPData(toClient, _packet);
            }
        }

    }
}
