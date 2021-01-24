using System;

namespace Ship.Shared.Utilities
{
    class Log
    {
        private static ELogLevel logLevel;
        private static Action<String> printMethod = defaultPrint;
        private static bool showTime = true;

        private Log()
        {

        }

        public static void setupLogger(ELogLevel _logLevel, bool _showTime, Action<String> _printMethod)
        {
            logLevel = _logLevel;
            printMethod = _printMethod;
            showTime = _showTime;
        }

        public static void setupLogger(ELogLevel _logLevel, bool _showTime)
        {
            logLevel = _logLevel;
            showTime = _showTime;
        }

        public static void setupLogger(ELogLevel _logLevel)
        {
            logLevel = _logLevel;
        }

        public static void debug(String msg)
        {
            if(logLevel <= ELogLevel.DEBUG)
            {
                format("[DEBUG]: ", msg);
            }
        }

        public static void info(String msg)
        {
            if (logLevel <= ELogLevel.INFO)
            {
                format("[INFO]:  ", msg);
            }
        }

        public static void error(String msg)
        {
            if (logLevel <= ELogLevel.ERROR)
            {
                format("[ERROR]: ", msg);
            }
        }
        private static String getTimeStamp()
        {
            return DateTime.Now + "\t";
        }

        private static void format(String tag, String msg)
        {
            String fullString = tag;
            if(showTime)
            {
                fullString += getTimeStamp();
            }

            fullString += msg;

            printMethod(fullString);
        }

        private static void defaultPrint(String msg)
        {
            Console.WriteLine(msg);
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
