using System;

namespace OnePlusOne
{
    partial class Program
    {
#pragma warning disable IDE0060 // 删除未使用的参数
        private static void Main(string[] args)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            try
            {
                Train.HumanVsAI("randomData.txt");
            }
            catch (Exception)
            {
                Console.WriteLine("LoadTrainData failed");
                Console.WriteLine("Press Enter to random train");
                Console.ReadLine();
                Train.RandomTrainTiming(10000);
                Train.HumanVsAI("randomData.txt");
            }
        }

    }
}
