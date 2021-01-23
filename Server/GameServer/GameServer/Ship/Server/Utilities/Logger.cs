using System;

namespace Ship.Server.Utilities
{
    class Logger
    {
        private static ELogLevel logLevel;

        private Logger()
        {

        }

        public static void setLogLevel(ELogLevel _logLevel)
        {
            logLevel = _logLevel;
        }

        public static void debug(String msg)
        {
            if(logLevel <= ELogLevel.DEBUG)
            {
                print("[DEBUG]: ", msg);
            }
        }

        public static void info(String msg)
        {
            if (logLevel <= ELogLevel.INFO)
            {
                print("[INFO]:  ", msg);
            }
        }

        public static void error(String msg)
        {
            if (logLevel <= ELogLevel.ERROR)
            {
                print("[ERROR]: ", msg);
            }
        }
        private static String getTimeStamp()
        {
            return DateTime.Now + "\t";
        }

        private static void print(String tag, String msg)
        {
            Console.WriteLine(tag + getTimeStamp() + msg);
        }

        public static String getLogLevelString()
        {
            switch(logLevel)
            {
                case ELogLevel.DEBUG: return "DEBUG";
                case ELogLevel.ERROR: return "ERROR";
                case ELogLevel.INFO: return "INFO";
                default: return "UNKNOWN";
            }
        }
    }
}
