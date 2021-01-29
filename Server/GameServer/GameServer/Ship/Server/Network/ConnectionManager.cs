using Ship.Network;
using Ship.Network.Transport;
using Ship.Shared.Utilities;

namespace Ship.Server.Network
{
    public class ConnectionManager
    {
        private ClientManager clientManager;

        public ConnectionManager() 
        {
            
        }

        public void init(ClientManager clientHandler)
        {
            this.clientManager = clientHandler;
        }

        #region send

        private void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            foreach (var client in clientManager.GetClients().Values) {
                client.tcp.SendData(_packet);
            }

            /*
            for (int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                GameServer.clients[i].tcp.SendData(_packet);
            }
            */
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

            /*
            _packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    GameServer.clients[i].tcp.SendData(_packet);
                }
            }
            */
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

            /*
            _packet.WriteLength();
            for (int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                GameServer.clients[i].udp.SendData(_packet);
            }
            */
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

            /*
            _packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    GameServer.clients[i].udp.SendData(_packet);
                }
            }
            */
        }

        private void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            clientManager.GetClients()[_toClient].udp.SendData(_packet);
        }

        #endregion


        public void OnClientConnected(int clientId)
        {
            using (Packet _packet = new Packet((int)PacketTypes.ServerPackets.ASSIGN_CLIENT_ID))
            {
                ClientId clientIdObj = new ClientId(clientId);
                clientIdObj.ToPacket(_packet);
                     
                SendTCPData(clientId, _packet);
            }
        }

    }
}
