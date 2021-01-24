﻿using System;
using System.Threading;
using Ship.Server.Network;
using Ship.Shared.Utilities;

namespace Ship.Server.Standalone
{
    class Program
    {
        private const int TICKS_PER_SEC = 30;
        private const int MS_PER_TICK = 1000 / TICKS_PER_SEC;
        private const int MAX_NUMBER_OF_CONNECTIONS = 100;
        private const int PORT = 26950;
        private const ELogLevel logLevel = ELogLevel.DEBUG;

        private static bool isRunning = false;

        static void Main(string[] args)
        {
            Log.setupLogger(logLevel);
            Log.info("Server started");
            Log.debug("Log level: " + Log.getLogLevelString());

            try
            {
                Console.Title = "Game Server";
                isRunning = true;

                Thread mainThread = new Thread(new ThreadStart(MainThread));
                mainThread.Start();

                Com.Start(PORT, MAX_NUMBER_OF_CONNECTIONS);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void MainThread()
        {
            Log.info($"Main thread started. Running at {TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;


            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {

                    ThreadManager.UpdateMain();

                    //GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine((e.ExceptionObject as Exception).Message);
        }
    }
}
