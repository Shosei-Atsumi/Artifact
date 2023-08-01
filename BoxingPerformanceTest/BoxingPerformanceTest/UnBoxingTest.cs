using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingPerformanceTest
{
    class UnBoxingTest
    {
        public void Run()
        {
            Run(10000);
        }

        public double Run(int NumOExecute)
        {
            Stopwatch sw = new Stopwatch();
            int num = 1;
            
            System.Console.WriteLine("##### 計測開始 ボックス化なし #####");

            sw.Start();
            for (int i = 0; i < NumOExecute; i++)
            {
                string res = $"numの値は{num.ToString()}";
            }
            sw.Stop();

            System.Console.WriteLine("経過時間: " + sw.Elapsed.TotalSeconds + "s");
            System.Console.WriteLine("###################################\n");

            return sw.Elapsed.TotalSeconds;
        }
    }
}
