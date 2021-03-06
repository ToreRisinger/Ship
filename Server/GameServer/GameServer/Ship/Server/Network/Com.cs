﻿using System;
using System.Net;
using System.Net.Sockets;
using Ship.Network;
using Ship.Utilities;

namespace Ship.Server.Network
{
    class Com
    {
        private static TcpListener tcpListener;
        private static UdpClient udpListener;
        private static int port;

        private static ClientManager clientManager;

        public static void Start(ClientManager _clientManager, int _port)
        {
            clientManager = _clientManager;
            port = _port;
            

            Log.info($"Setting up connection listener.");

            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            udpListener = new UdpClient(port);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            Log.info($"Now listening on port {port}.");
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            clientManager.incommingConnection(_client);
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

                    if(clientManager.GetClients()[_clientId].udp.endPoint == null)
                    {
                        clientManager.GetClients()[_clientId].udp.Connect(_clientEndPoint);
                        return;
                    }

                    if(clientManager.GetClients()[_clientId].udp.endPoint.ToString() == _clientEndPoint.ToString())
                    {
                        clientManager.GetClients()[_clientId].udp.HandleData(_packet);
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
