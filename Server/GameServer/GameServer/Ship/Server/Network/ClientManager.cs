
using Ship.Shared.Utilities;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Ship.Server.Network
{
    public class ClientManager
    {
        private Dictionary<int, Client> clients = new Dictionary<int, Client>();
        private PacketHandler packetHandler;
        private ConnectionManager connectionManager;


        public ClientManager()
        {
            
        }


        public void init(ConnectionManager connectionManager, PacketHandler packetHandler)
        {
            this.connectionManager = connectionManager;
            this.packetHandler = packetHandler;
            initilizeClients(connectionManager);
        }

        public bool incommingConnection(TcpClient tcpClient)
        {
            Log.info($"Incomming connection from {tcpClient.Client.RemoteEndPoint}...");

            for (int i = 1; i <= Com.GetMaxConnections(); i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(tcpClient);
                    return true;
                }
            }

            Log.info($"{tcpClient.Client.RemoteEndPoint} failed to connect: Server full!");
            return false;
        }

        public Dictionary<int, Client> GetClients() {
            return clients;
        }


        private void initilizeClients(ConnectionManager connectionManager)
        {
            for (int i = 1; i <= Com.GetMaxConnections(); i++)
            {
                clients.Add(i, new Client(i, connectionManager, packetHandler));
            }
        }
    }
}
