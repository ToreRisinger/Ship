
using Ship.Shared.Utilities;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Ship.Server.Network
{
    class ClientHandler
    {
        private int maxPlayers = 100;
        private Dictionary<int, Client> clients = new Dictionary<int, Client>();

        public ClientHandler(int maxPlayers)
        {
            initilizeClients();
        }

        public bool incommingConnection(TcpClient tcpClient)
        {
            Log.info($"Incomming connection from {tcpClient.Client.RemoteEndPoint}...");

            for (int i = 1; i <= maxPlayers; i++)
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


        private void initilizeClients()
        {
            for (int i = 1; i <= maxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }
        }
    }
}
