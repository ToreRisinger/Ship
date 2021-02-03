

namespace Ship.Utilities
{
    public class IdGenerator
    {

        private static int clientId = 1000000000;
        private static int serverId = 0;


        private IdGenerator()
        {

        }

        public static int getClientId()
        {
            return clientId++;
        }

        public static int getServerId()
        {
            return serverId++;
        }

    }
}
