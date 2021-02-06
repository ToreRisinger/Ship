
namespace Ship.Network.Transport
{
    public class ServerErrorTp : Transportable
    {
        public EServerErrorCode errorCode;

        public ServerErrorTp(EServerErrorCode errorCode)
        {
            this.errorCode = errorCode;
        }

        public ServerErrorTp(Packet _packet) : base(_packet)
        {
            errorCode = (EServerErrorCode)_packet.ReadInt();
        }

        public override void ToPacket(Packet _packet)
        {
            _packet.Write((int)errorCode);
        }
    }
}
