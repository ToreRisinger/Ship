
namespace Ship.Network.Transport
{
    public class ServerError
    {
        public EServerErrorCode errorCode;

        public ServerError(EServerErrorCode errorCode)
        {
            this.errorCode = errorCode;
        }

        public static ServerError FromPacket(Packet _packet)
        {
            EServerErrorCode readErrorCode = (EServerErrorCode)_packet.ReadInt();
            return new ServerError(readErrorCode);
        }

        public void ToPacket(Packet _packet)
        {
            _packet.Write((int)errorCode);
        }
    }
}
