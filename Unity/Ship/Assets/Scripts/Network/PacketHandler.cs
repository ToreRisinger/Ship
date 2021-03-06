﻿using Ship.Network;
using Ship.Network.Transport;
using Ship.Utilities;
using System.Collections.Generic;
using static Ship.Network.PacketTypes;

public class PacketHandler
{
    private static PacketHandler instance;
    private static ConnectionManager connectionManager;
    private delegate void PacketHandlerFunction(Packet _packet);
    private Dictionary<int, PacketHandlerFunction> packetHandlers;

    public static PacketHandler GetInstance()
    {
        if (instance == null)
        {
            instance = new PacketHandler();

        }

        return instance;
    }

    private PacketHandler()
    {
        connectionManager = ConnectionManager.GetInstance();
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
    public void onServerError(Packet packet)
    {
        ServerError serverError = new ServerError(packet);
        connectionManager.onServerError(serverError);
    }

    public void onReceiveClientId(Packet packet)
    {
        ClientId clientId = new ClientId(packet);
        connectionManager.onReceiveClientId(clientId);
    }

    public void onReceiveInitialLoad(Packet packet)
    {
        InitialLoad initialLoad = new InitialLoad(packet);
        connectionManager.onReceiveInitialLoad(initialLoad);
    }

    public void onReceiveGameState(Packet packet)
    {
        GameState gameState = new GameState(packet);
        connectionManager.onReceiveGameState(gameState);
    }

    private void initializePacketHandlers()
    {
        packetHandlers = new Dictionary<int, PacketHandlerFunction>()
        {
            { (int)ServerPackets.SERVER_ERROR, onServerError },
            { (int)ServerPackets.ASSIGN_CLIENT_ID, onReceiveClientId },
            { (int)ServerPackets.INITIAL_LOAD, onReceiveInitialLoad },
            { (int)ServerPackets.GAME_STATE, onReceiveGameState },

        };

    }
}
