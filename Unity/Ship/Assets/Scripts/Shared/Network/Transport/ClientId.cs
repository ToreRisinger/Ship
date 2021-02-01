namespace Ship.Network.Transport
{
    public class ClientId : Transportable
    {
        public int clientId;

        public ClientId(int _clientId)
        {
            clientId = _clientId;
        }

        public ClientId(Packet _packet) : base(_packet)
        {
            clientId = _packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write(clientId);
        }
    }
}
