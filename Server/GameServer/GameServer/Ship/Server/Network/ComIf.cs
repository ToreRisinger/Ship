using Ship.Network;
using System.Collections.Generic;

namespace Ship.Server.Network
{
    class ComIf
    {
        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandler> packetHandlers = new Dictionary<int, PacketHandler>()
        {
           //{ (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
            //{ (int)ClientPackets.playerCommand, ServerHandle.PlayerCommand },
        };

        private ComIf()
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
