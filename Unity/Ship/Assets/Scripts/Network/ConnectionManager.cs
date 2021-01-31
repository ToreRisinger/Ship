using Ship.Network;
using Ship.Network.Transport;
using Ship.Shared.Utilities;
using System;

public class ConnectionManager
{
    private static ConnectionManager instance;
    private Client client;

    public static ConnectionManager GetInstance()
    {
        if (instance == null)
        {
            instance = new ConnectionManager();
        }

        return instance;
    }

    private ConnectionManager()
    {
        client = Client.GetInstance();
    }


#region send

private void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        client.tcp.SendData(_packet);

    }

    private void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        client.udp.SendData(_packet);
    }

    #endregion

    private void OnApplicationQuit()
    {
        client.Disconnect();
    }

    public void ConnectToGameServer()
    {
        client.ConnectToGameServer();
    }

    public void onServerError(ServerError serverError)
    {
        Log.error("Server error: " + serverError.errorCode);
    }

    public void onReceiveClientId(ClientId clientIdObj)
    {
        using (Packet _packet = new Packet((int)PacketTypes.ClientPackets.CLIENT_ID_RECEIVED))
        {
            //clientIdObj.ToPacket(_packet);
            ClientId aa = new ClientId(34);
            aa.ToPacket(_packet);
            SendTCPData(_packet);
        }
    }
}
