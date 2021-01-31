using Ship.Network;
using Ship.Shared.Utilities;
using System;
using System.Net;
using System.Net.Sockets;

namespace Ship.Server.Network
{
    public class Client
    {
        public static int dataBufferSize = 4096;

        public int id;
        public TCP tcp;
        public UDP udp;

        private ConnectionManager connectionManager;
        private PacketHandler packetHandler;

        public Client(int _clientId, ConnectionManager connectionManager, PacketHandler packetHandler)
        {
            id = _clientId;
            this.connectionManager = connectionManager;
            this.packetHandler = packetHandler;
            tcp = new TCP(this, id);
            udp = new UDP(this, id);
        }

        public class TCP
        {
            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            private Packet receivedData;
            private byte[] receiveBuffer;
            private Client client;

            public TCP(Client client, int _id)
            {
                this.client = client;
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receivedData = new Packet();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                client.Connect();
            }

            public void SendData(Packet _packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }

                } catch(Exception _ex)
                {
                    Log.error($"Error sending data to player {id} via TCP: {_ex}");
                }
            }

            private void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if(_byteLength <= 0)
                    {
                        client.Disconnect();
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    receivedData.Reset(HandleData(_data));
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

                } catch (Exception _ex)
                {
                    Log.error($"Error receiving TCP data: {_ex}.");
                    client.Disconnect();
                }
            }

            private bool HandleData(byte[] _data)
            {
                int _packetLength = 0;

                receivedData.SetBytes(_data);

                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }

                while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
                {
                    byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet _packet = new Packet(_packetBytes))
                        {
                            int _packetId = _packet.ReadInt();
                            client.packetHandler.onPacketReceived(_packetId, id, _packet);
                        }
                    });

                    _packetLength = 0;
                    if (receivedData.UnreadLength() >= 4)
                    {
                        _packetLength = receivedData.ReadInt();
                        if (_packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                return _packetLength <= 1;
            }

            public void Disconnect()
            {
                if (socket != null)
                {
                    socket.Close();
                }
                stream = null;
                receivedData = null;
                receiveBuffer = null;
                socket = null;
            }
        }

        public class UDP
        {
            public IPEndPoint endPoint;
            private int id;
            private Client client;

            public UDP(Client client, int _id)
            {
                this.client = client;
                id = _id;
            }

            public void Connect(IPEndPoint _endPoint)
            {
                endPoint = _endPoint;
            }

            public void SendData(Packet _packet)
            {
                Com.SendUDPData(endPoint, _packet);
            }

            public void HandleData(Packet _packetData)
            {
                int _packetLength = _packetData.ReadInt();
                byte[] _packetBytes = _packetData.ReadBytes(_packetLength);

                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using(Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        client.packetHandler.onPacketReceived(_packetId, id, _packet);
                    }
                });
            }

            public void Disconnect()
            {
                endPoint = null;
            }
        }

        private void Disconnect()
        {
            if(tcp.socket != null)
            {
                Log.info($"{tcp.socket.Client.RemoteEndPoint} has disconnected.");
            } 
            else
            {
                Log.info($"Client has disconnected.");
            }
            
            //GameLogic.onPlayerLeft(id);
            tcp.Disconnect();
            udp.Disconnect();
        }

        private void Connect()
        {
            Log.info($"{tcp.socket.Client.RemoteEndPoint} has connected.");
            connectionManager.OnClientConnected(id);
            
        }
    }
}
