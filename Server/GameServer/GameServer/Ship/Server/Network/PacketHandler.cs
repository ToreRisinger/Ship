using Ship.Network;
using System.Collections.Generic;

namespace Ship.Server.Network
{
    class PacketHandler
    {
        public delegate void PacketHandlerFunction(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandlerFunction> packetHandlers = new Dictionary<int, PacketHandlerFunction>()
        {
           //{ (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
            //{ (int)ClientPackets.playerCommand, ServerHandle.PlayerCommand },
        };

        private PacketHandler()
        {

        }

        public static void onPacketReceived(int packetId, int clientId, Packet packet)
        {
            //TODO add check
            packetHandlers[packetId](clientId, packet);
        }

        //Handle functions




    }
}
