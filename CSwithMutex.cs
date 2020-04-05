using System;
using System.Threading;

namespace cigarette_smokers_lab
{
    class CSwithMutex
    {
        
        private static Mutex Mut = new Mutex(false);

        private static AutoResetEvent signalFromA = new AutoResetEvent(false);
        private static AutoResetEvent signalFromB = new AutoResetEvent(false);
        private static AutoResetEvent signalFromC = new AutoResetEvent(false);

        private static int tobacco = 0, match = 0, paper = 0;

        public void Beginning()
        {
            Thread StartAgents = new Thread(startAgents);
            Thread smokerMatchFun = new Thread(smokerMatch);
            Thread smokerTbaccoFun = new Thread(smokerTobbaco);
            Thread smokerPaperFun = new Thread(smokerPaper);

            smokerMatchFun.Start();
            smokerTbaccoFun.Start();
            smokerPaperFun.Start();
            StartAgents.Start();

        }
        static void startAgents()
        {
            while (true)
            {
                Mut.WaitOne();
                int random = new Random().Next(1, 4);
                if (random == 1)
                {
                    Console.WriteLine("Now is Agent A with paper and tobacco .");
                    Console.WriteLine($"Tobacco is Active and its value is : {++tobacco} ");
                    Console.WriteLine($"Paper is Active and its value is : {++paper}");
                    Thread.Sleep(new Random().Next(100, 2000));
                    Console.WriteLine("Wakeup signal sent to Match Smokker");
                    signalFromA.Set();

                }
                else if (random == 2)
                {
                    Console.WriteLine("Now is agent B with paper and matches .");
                    Console.WriteLine($"Match is Active and its value is : {++match}"); ;
                    Console.WriteLine($"Paper is Active and its value is : {++paper}");
                    Thread.Sleep(new Random().Next(100, 2000));
                    Console.WriteLine("Wakeup signal sent to Tobacco Smokker");
                    signalFromB.Set();
                }
                else
                {
                    Console.WriteLine("Now is agent C with tobacco and matches .");
                    Console.WriteLine($"Match is Active and its value is : {++match}");
                    Console.WriteLine($"Tobacco is Active and its value is : {++tobacco}");
                    Thread.Sleep(new Random().Next(100, 2000));
                    Console.WriteLine("Wakeup signal sent to Paper Smokker");
                    signalFromC.Set();
                }
                Mut.ReleaseMutex();

            }


        }

        static void smokerMatch()
        {
            while (true)
            {
                signalFromA.WaitOne();
                Mut.WaitOne();

                Console.WriteLine($"Smoker Match is making Cigarette by Tobacco and Paper . \n Tobacco value now is : {--tobacco} , Paper value now is : {--paper}");
                Thread.Sleep(new Random().Next(100, 2000));

                Mut.ReleaseMutex();
                signalFromA.Reset();
                Console.WriteLine("Smoker Match is smoking  ....");
                Thread.Sleep(new Random().Next(100, 2000));
            }
        }

        static void smokerTobbaco()
        {
            while (true)
            {
                signalFromB.WaitOne();
                Mut.WaitOne();
                Console.WriteLine($"Smoker Tobacco is making Cigarette by Match and Paper .\n Match value now is : {--match} , Paper value now is : {--paper}");
                Thread.Sleep(new Random().Next(100, 2000));
                signalFromB.Reset();
                Mut.ReleaseMutex();
                Console.WriteLine("Smoker Tobacco is smoking  ....");
                Thread.Sleep(new Random().Next(100, 2000));
            }

        }


        static void smokerPaper()
        {
            while (true)
            {
                signalFromC.WaitOne();
                Mut.WaitOne();
                Console.WriteLine($"Smoker Paper is making Cigarette by Match and Tobacco .\n Match value now is : {--match} , Tobacco value now is : {--tobacco}");
                Thread.Sleep(new Random().Next(100, 2000));
                signalFromC.Reset();
                Mut.ReleaseMutex();
                Console.WriteLine("Smoker Paper is smoking  ....");
                Thread.Sleep(new Random().Next(100, 2000));
            }

        }

    }
}