
namespace Ship.Network
{
    class PacketTypes
    {
        /// <summary>
        /// Sent from server to client.
        /// </summary>
        public enum ServerPackets
        {
            SERVER_ERROR = 1,
            ASSIGN_CLIENT_ID,
            INITIAL_LOAD,
            GAME_STATE,
        }

        /// <summary>
        /// Sent from client to server.
        /// </summary>
        public enum ClientPackets
        {
            CLIENT_ID_RECEIVED = 1,
            PLAYER_COMMAND,
        }

    }
}
