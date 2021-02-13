using System;

namespace Utils
{
    public class Utilities
    {
        private static Random random = new Random();
        private Utilities()
        {
            
        }

        public static int rand(int from, int to)
        {
            
            return random.Next(from, to + 1);
        }

    }
}
