using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBurner
{
    /// <summary>
    /// Adapted from http://stackoverflow.com/questions/2514544/simulate-steady-cpu-load-and-spikes
    /// </summary>
	class Program
    {
        volatile static bool keepRunning = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting CPU burn");

            var cpuUsage = int.Parse(Environment.GetEnvironmentVariable("CPU_USAGE_PERCENTAGE") ?? "50");
            var runTime = TimeSpan.FromSeconds(int.Parse(Environment.GetEnvironmentVariable("RUN_TIME_IN_SECONDS") ?? "10"));

            // spin up a thread for each proc
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                Console.WriteLine($"Starting thread #{i + 1}");
                Thread t = new Thread(BurnMyCpu);
                t.Start(cpuUsage);
                threads.Add(t);
            }

            // let threads run for specfied amount of time, then abort
            Thread.Sleep(runTime);
            keepRunning = false;
            Console.WriteLine("Completed CPU burn, press any key to exit");

            // just wait forever
            Console.ReadLine();
        }

        public static void BurnMyCpu(object cpuUsage)
        {
            Parallel.For(0, 1, i =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                while (keepRunning)
                {
                    if (watch.ElapsedMilliseconds > (int)cpuUsage)
                    {
                        Thread.Sleep(100 - (int)cpuUsage);
                        watch.Reset();
                        watch.Start();
                    }
                }
            });
        }
    }
}