using Ship.Game;
using Ship.Game.Event;
using Ship.Network;
using Ship.Network.Transport;
using Ship.Utilities;
using System;
using UnityEngine.SceneManagement;

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
        //TODO this is not called
        client.Disconnect();
    }

    public void ConnectToGameServer()
    {
        client.ConnectToGameServer();
    }

    public void OnDisconnect()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #region RX

    public void onServerError(ServerError serverError)
    {
        Log.error("Server error: " + serverError.errorCode);
        client.Disconnect();
    }

    public void onReceiveClientId(ClientId clientIdObj)
    {
        GameManager.instance.OnPlayerIdAssigned(clientIdObj.clientId);

        using (Packet _packet = new Packet((int)PacketTypes.ClientPackets.CLIENT_ID_RECEIVED))
        {
            clientIdObj.ToPacket(_packet);
            SendTCPData(_packet);
        }
    }

    public void onReceiveInitialLoad(InitialLoad initialLoad)
    {
        GameManager.instance.OnInitialLoad(initialLoad);
    }

    public void onReceiveGameState(GameState gameState)
    {
        while (gameState.events.Count > 0)
        {
            
            EventObject evnt = gameState.events.Dequeue();
            Log.debug("EVENT: " + evnt);
            EventManager.instance.PushEvent(evnt);
        }

        GameManager.instance.OnNewGameState(gameState);
    }

    #endregion

    #region TX

    public void sendPlayerCommand(PlayerCommand playerCommand)
    {
        using (Packet _packet = new Packet((int)PacketTypes.ClientPackets.PLAYER_COMMAND))
        {
            playerCommand.ToPacket(_packet);
            SendTCPData(_packet);
        }
    }

    #endregion
}
