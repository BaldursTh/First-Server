using System;
using System.Threading;

namespace GameServer
{
    class Program
    {
        private static bool isRunning;
        static void Main(string[] args)
        {
            Console.Title = "Epic Server";

            Server.Start(50, 36416);
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
            Console.ReadKey();
        }
        private static void MainThread()
        {
            Console.WriteLine($"Main thread has started. Running at {Constants.Tick_Per_Second} ticks per sec");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while(_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();
                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_Per_Tick);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
