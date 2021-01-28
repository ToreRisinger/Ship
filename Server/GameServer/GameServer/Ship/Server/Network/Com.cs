using System;
using System.Net;
using System.Net.Sockets;
using Ship.Network;
using Ship.Shared.Utilities;

namespace Ship.Server.Network
{
    class Com
    {
        private static TcpListener tcpListener;
        private static UdpClient udpListener;
        private static int port;
        private static int maxConnections;
        private static ClientHandler clientHandler;

        public static void Start(int _port, int _maxConnections)
        {
            port = _port;
            maxConnections = _maxConnections;
            clientHandler = ClientHandler.GetInstance();

            Log.info($"Setting up connection listener.");

            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            udpListener = new UdpClient(port);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            Log.info($"Now listening on port {port}.");
        }

        public static int GetMaxConnections()
        {
            return maxConnections;
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            clientHandler.incommingConnection(_client);
        }

        private static void UDPReceiveCallback(IAsyncResult _result)
        {
            try
            {
                IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] _data = udpListener.EndReceive(_result, ref _clientEndPoint);
                udpListener.BeginReceive(UDPReceiveCallback, null);

                if(_data.Length < 4)
                {
                    return;
                }

                using (Packet _packet = new Packet(_data))
                {
                    
                    int _clientId = _packet.ReadInt();

                    if(_clientId == 0)
                    {
                        return;
                    }

                    if(clientHandler.GetClients()[_clientId].udp.endPoint == null)
                    {
                        clientHandler.GetClients()[_clientId].udp.Connect(_clientEndPoint);
                        return;
                    }

                    if(clientHandler.GetClients()[_clientId].udp.endPoint.ToString() == _clientEndPoint.ToString())
                    {
                        clientHandler.GetClients()[_clientId].udp.HandleData(_packet);
                    }
                    
                }
            } catch (Exception _ex)
            {
                Log.error($"Error receiving UDP data: {_ex}");
            }
        }

        public static void SendUDPData(IPEndPoint _clientEndPoint, Packet _packet)
        {
            try
            {
                if(_clientEndPoint != null)
                {
                    udpListener.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);
                }
            } catch(Exception _ex)
            {
                Log.error($"Error sending data to {_clientEndPoint} via UDP: {_ex}");
            }
        }
    }
}
