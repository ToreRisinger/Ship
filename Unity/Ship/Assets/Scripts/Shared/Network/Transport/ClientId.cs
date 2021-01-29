using Ship.Network;

namespace Ship.Network.Transport
{
    public class ClientId
    {
        public int clientId;

        public ClientId(int _clientId)
        {
            clientId = _clientId;
        }

        public static ClientId FromPacket(Packet _packet)
        {
            int readClientId = _packet.ReadInt();
            return new ClientId(readClientId);
        }

        public void ToPacket(Packet _packet)
        {
            _packet.Write(clientId);
        }
    }
}
