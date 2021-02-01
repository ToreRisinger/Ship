using Server.Game;
using Ship.Game.Model;
using Ship.Network;
using Ship.Network.Transport;

namespace Ship.Server.Network
{
    public class ConnectionManager
    {
        private ClientManager clientManager;
        private GameManager gameManager;

        public ConnectionManager() 
        {
            
        }

        public void init(ClientManager clientHandler, GameManager gameManager)
        {
            this.clientManager = clientHandler;
            this.gameManager = gameManager;
        }

        public void update()
        {
            //TODO each tick, get gamestate for each client and events from GameManager and send to all clients
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
                gameManager.OnClientJoin(fromClient, "username");
            }
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

    }
}
