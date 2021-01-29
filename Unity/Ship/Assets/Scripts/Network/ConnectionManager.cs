using Ship.Network;
using Ship.Network.Transport;
using Ship.Shared.Utilities;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager instance;
    private Client client;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Log.error("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        client = new Client();
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

    public void onReceiveClientId(ClientId clientIdObj)
    {
        //TODO
    }


}
