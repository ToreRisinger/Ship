
namespace Ship.Network.Transport
{
    public class ServerError : Transportable
    {
        public EServerErrorCode errorCode;

        public ServerError(EServerErrorCode errorCode)
        {
            this.errorCode = errorCode;
        }

        public ServerError(Packet _packet) : base(_packet)
        {
            errorCode = (EServerErrorCode)_packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write((int)errorCode);
        }
    }
}
