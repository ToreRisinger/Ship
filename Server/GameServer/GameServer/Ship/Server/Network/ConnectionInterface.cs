using Ship.Network;
using Ship.Network.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Server.Network
{
    class ConnectionInterface
    {
        private static ConnectionInterface instance;
        private static ClientHandler clientHandler;

        public static ConnectionInterface GetInstance()
        {
            if(instance == null)
            {
                instance = new ConnectionInterface();
            }

            return instance;
        }

        private ConnectionInterface()
        {
            clientHandler = ClientHandler.GetInstance();
        }

        #region send

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            foreach (var client in clientHandler.GetClients().Values) {
                client.tcp.SendData(_packet);
            }

            /*
            for (int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                GameServer.clients[i].tcp.SendData(_packet);
            }
            */
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            foreach (var client in clientHandler.GetClients().Values)
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

        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            clientHandler.GetClients()[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            foreach (var client in clientHandler.GetClients().Values)
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

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            foreach (var client in clientHandler.GetClients().Values)
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

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            clientHandler.GetClients()[_toClient].udp.SendData(_packet);
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
