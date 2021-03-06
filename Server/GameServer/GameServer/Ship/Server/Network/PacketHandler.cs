﻿using Ship.Network;
using Ship.Network.Transport;
using System.Collections.Generic;
using static Ship.Network.PacketTypes;

namespace Ship.Server.Network
{
    public class PacketHandler
    {
        private ConnectionManager connectionManager;
        private delegate void PacketHandlerFunction(int _fromClient, Packet _packet);
        private static Dictionary<int, PacketHandlerFunction> packetHandlers;

        public PacketHandler()
        {
            
        }

        public void init(ConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            initializePacketHandlerFunctions();
        }

        public void onPacketReceived(int packetId, int clientId, Packet packet)
        {
            //TODO add check
            packetHandlers[packetId](clientId, packet);
        }

        /*
         * Handle functions
         * 
         */
        private void OnClientIdReceived(int fromClient, Packet packet)
        {
            ClientId clientId = new ClientId(packet);
            connectionManager.OnClientIdReceived(fromClient, clientId);
        }

        private void OnPlayerCommandReceived(int fromClient, Packet packet)
        {
            PlayerCommand playerCommand = new PlayerCommand(packet);
            connectionManager.OnPlayerCommandReceived(playerCommand);
        }


        private void initializePacketHandlerFunctions()
        {
            packetHandlers = new Dictionary<int, PacketHandlerFunction>()
            {
                { (int)ClientPackets.CLIENT_ID_RECEIVED, OnClientIdReceived},
                { (int)ClientPackets.PLAYER_COMMAND, OnPlayerCommandReceived}
            };
        }
    }
}
