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
                        totalTimeTicks += sw.ElapsedMilliseconds;
                        totalTimeMilli += sw.ElapsedMilliseconds;
                    };
                }catch(Exception e) {
                    Console.WriteLine("\n"+e.ToString());
                    return;
                }
            }
            Console.WriteLine("Average Ticks: "+(totalTimeTicks / numOfRuns));
            Console.WriteLine("Average Milliseconds: " + (totalTimeMilli / numOfRuns));
        }
    }
}
