using System;
using System.Diagnostics;

namespace ExeTester {
    class Program {
        static void Main(string[] args) {
            int numOfRuns = int.Parse(args[0]);
            string path = args[1];
            string parameters = "";
            int l = args.Length;
            for(int  i = 2; i < l; i++) {
                parameters += args[i]+" ";
            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = path;
            startInfo.Arguments = parameters;
            long longestTicks = 0;
            long longestMilli = 0;
            long shortestTicks = long.MaxValue;
            long shortestMilli = long.MaxValue;
            long totalTimeTicks = 0;
            long totalTimeMilli = 0;
            Stopwatch sw;
            for (int i = 0; i < numOfRuns; i++) {
                sw = new Stopwatch();
                sw.Start();
                try {
                    using (Process process = Process.Start(startInfo)) {
                        process.WaitForExit();
                        sw.Stop();
                        totalTimeTicks += sw.ElapsedTicks;
                        totalTimeMilli += sw.ElapsedMilliseconds;
                        if(sw.ElapsedTicks > longestTicks) {
                            longestTicks = sw.ElapsedTicks;
                            longestMilli = sw.ElapsedMilliseconds;
                        }else if(sw.ElapsedTicks < shortestTicks) {
                            shortestTicks = sw.ElapsedTicks;
                            shortestMilli = sw.ElapsedMilliseconds;
                        }
                    };
                }catch(Exception e) {
                    Console.WriteLine("\n"+e.ToString());
                    return;
                }
            }
            Console.WriteLine("\nShortest Ticks taken: " + shortestTicks);
            Console.WriteLine("Shortest Milliseconds taken: " + shortestMilli);
            Console.WriteLine("\nAverage Ticks taken: "+(totalTimeTicks / numOfRuns));
            Console.WriteLine("Average Milliseconds taken: " + (totalTimeMilli / numOfRuns));
            Console.WriteLine("\nLongest Ticks taken: " + longestTicks);
            Console.WriteLine("Longest Milliseconds taken: " + longestMilli);
        }
    }
}
