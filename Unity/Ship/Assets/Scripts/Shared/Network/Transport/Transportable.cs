namespace Ship.Network.Transport
{
    public abstract class Transportable
    {
        public Transportable()
        {

        }

        public Transportable(Packet _packet)
        {

        }

        abstract public void ToPacket(Packet _packet);
    }
}
