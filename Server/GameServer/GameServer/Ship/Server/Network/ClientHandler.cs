
using Ship.Shared.Utilities;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Ship.Server.Network
{
    class ClientHandler
    {
        private Dictionary<int, Client> clients = new Dictionary<int, Client>();
        private static ClientHandler instance;

        public static ClientHandler GetInstance()
        {
            if(instance == null)
            {
                instance = new ClientHandler();
            }

            return instance;
        }

        private ClientHandler()
        {
            initilizeClients();
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


        private void initilizeClients()
        {
            for (int i = 1; i <= Com.GetMaxConnections(); i++)
            {
                clients.Add(i, new Client(i));
            }
        }
    }
}
