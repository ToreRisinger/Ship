
using Ship.Utilities;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Ship.Server.Network
{
    public class ClientManager
    {
        private int TIMEOUT_TIME = 10000; //10 sec

        private Dictionary<int, Client> clients = new Dictionary<int, Client>();
        private Dictionary<int, int> timeouts = new Dictionary<int, int>();

        private PacketHandler packetHandler;
        private ConnectionManager connectionManager;

        private int maxNrOfClients;


        public ClientManager()
        {
            
        }

        public void update(int ms)
        {
            foreach(var client in clients)
            {
                if(client.Value.isConnected())
                {
                    timeouts[client.Value.id] += ms;
                    if(timeouts[client.Value.id] > TIMEOUT_TIME)
                    {
                        //client.Value.Disconnect();
                    }
                }
            }
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
                timeouts.Add(i, 0);
            }
        }
    }
}
