using System;
using System.Threading;


namespace PSTest
{
    class Program
    {
        private static object _locker = new object ();
        private static int state;
        private static Random random = new Random();
        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(2, 2);

            for (int i = 0; i < 20; i++)
            {
                Thread thread1 = new Thread(ChangeState);
                thread1.Start();

                Thread.Sleep(100);

                Thread thread2 = new Thread(EvaluateState);
                thread2.Start();

                thread1.Join();
                thread2.Join();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Program finished");
            Console.Read();
        }

        private static void ChangeState()
        {
            for(int i = 0; i< 10000000; i++)
            {
                lock (_locker)
                {
                    state = random.Next(0, 5);
                }
            }
        }

        private static void EvaluateState()
        {
            lock (_locker)
            {
                switch (state)
                {
                    case 0:
                        Thread.Sleep(100);
                        WriteColoredLine(0, state);
                        break;
                    case 1:
                        Thread.Sleep(100);
                        WriteColoredLine(1, state);
                        break;
                    case 2:
                        Thread.Sleep(100);
                        WriteColoredLine(2, state);
                        break;
                    case 3:
                        Thread.Sleep(100);
                        WriteColoredLine(3, state);
                        break;
                    case 4:
                        Thread.Sleep(100);
                        WriteColoredLine(4, state);
                        break;
                    default:
                        Thread.Sleep(100);
                        WriteColoredLine(5, state);
                        break;
                }
            }
        }

        private static void WriteColoredLine(int evaluation, int currentState)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (evaluation != currentState)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Evaluated value = {evaluation}, Current state value = {currentState}");
        }
    }
}
