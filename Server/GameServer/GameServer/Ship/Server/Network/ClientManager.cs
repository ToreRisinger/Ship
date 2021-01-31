
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

        private int maxNrOfClients;


        public ClientManager()
        {
            
        }


        public void init(ConnectionManager connectionManager, PacketHandler packetHandler, int maxNrOfClients)
        {
            this.connectionManager = connectionManager;
            this.packetHandler = packetHandler;
            this.maxNrOfClients = maxNrOfClients;
            initilizeClients(connectionManager);
        }

        public bool incommingConnection(TcpClient tcpClient)
        {
            Log.info($"Incomming connection from {tcpClient.Client.RemoteEndPoint}...");

            for (int i = 1; i <= maxNrOfClients; i++)
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
            for (int i = 1; i <= maxNrOfClients; i++)
            {
                clients.Add(i, new Client(i, connectionManager, packetHandler));
            }
        }
    }
}
