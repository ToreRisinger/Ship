namespace Ship.Network.Transport
{
    public class ClientIdTp : Transportable
    {
        public int clientId;

        public ClientIdTp(int _clientId)
        {
            clientId = _clientId;
        }

        public ClientIdTp(Packet _packet) : base(_packet)
        {
            clientId = _packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write(clientId);
        }
    }
}
