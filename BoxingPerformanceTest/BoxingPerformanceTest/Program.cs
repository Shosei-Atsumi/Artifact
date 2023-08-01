using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 5; i++)
                Program.Run();

            Console.ReadLine();
        }

        private static void Run()
        {
            int n = 100000000;
            System.Console.WriteLine("###################################################################");
            System.Console.WriteLine($"実行回数: {String.Format("{0:#,0}", n)}回\n");

            var timeForBoxing = new BoxingTest().Run(n);
            var timeForUnBoxing = new UnBoxingTest().Run(n);

            System.Console.WriteLine($"実行時間{timeForBoxing / timeForUnBoxing}倍\n");
            System.Console.WriteLine("###################################################################");
            System.Console.WriteLine("\n");
        }
    }
}
