using Ship.Network;
using Ship.Network.Transport;
using Ship.Shared.Utilities;
using System.Collections.Generic;
using UnityEngine;
using static Ship.Network.PacketTypes;

public class PacketHandler : MonoBehaviour
{
    public static PacketHandler instance;
    public static ConnectionManager connectionManager;
    public delegate void PacketHandlerFunction(Packet _packet);
    public Dictionary<int, PacketHandlerFunction> packetHandlers;

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
        connectionManager = ConnectionManager.instance;
        initializePacketHandlers();
    }

    public void onPacketReceived(int packetId, Packet packet)
    {
        if(packetHandlers.ContainsKey(packetId))
        {
            packetHandlers[packetId](packet);
        } else
        {
            Log.error("Received Packet with id '" + packetId + "' not mapped to a packet handle function. Packet is ignored.");
        }
        
    }

    public void onReceiveClientId(Packet packet)
    {
        ClientId clientId = ClientId.FromPacket(packet);
        connectionManager.onReceiveClientId(clientId);
    }

    private void initializePacketHandlers()
    {
        packetHandlers = new Dictionary<int, PacketHandlerFunction>()
        {
            { (int)ServerPackets.ASSIGN_CLIENT_ID, onReceiveClientId }
        };

    }
}
