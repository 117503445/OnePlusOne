using System;
using System.Collections.Generic;
using System.IO;

namespace OnePlusOne
{
    partial class Program
    {
        private static Records records = new Records();
        /// <summary>
        /// 计时
        /// </summary>
        /// <param name="action"></param>
        private static void Timing(Action action)
        {
            var beginTime = DateTime.Now;
            action();
            var endTime = DateTime.Now;
            Console.WriteLine($"Start at {beginTime}\nEnd at   {endTime}\n{(endTime - beginTime).TotalMilliseconds}ms");
        }
        /// <summary>
        /// 由1个 RandomPlayer 进行多次自我随机对弈训练
        /// </summary>
        private static void RandomTrain()
        {
            Logger.Clear();
            Logger.IsEnabled = false;
            int trainNums = 10000;//训练次数
            int maxStep = 120;//单次训练允许的最大回合,一般1000次训练中单词最大回合数小于100
            for (int trainIndex = 0; trainIndex < trainNums; trainIndex++)
            {
                Console.WriteLine($"{trainIndex}/{trainNums}");

                GCase c = new GCase();
                RandomPlayer player = new RandomPlayer();

                List<string> cases = new List<string>();
                List<int> methods = new List<int>();


                for (int i = 0; i < maxStep; i++)
                {
                    if (c.GState != GState.Playing)
                    {
                        //Console.WriteLine(i);
                        break;
                    }
                    else
                    {
                        Logger.WriteLine("---");
                        Logger.WriteLine(c);
                        cases.Add(c.ToString());
                        int method = player.GetAddMethod(c.Nums);
                        Logger.WriteLine(method);
                        c.RunMethod(method);
                        methods.Add(method);
                        Logger.WriteLine(c);
                        c.Reserve();
                        Logger.WriteLine("Reserve");
                        Logger.WriteLine(c);
                    }
                    Logger.WriteLine("---");
                    Logger.WriteLine();
                }

                Logger.WriteLine("GameOver");
                Logger.WriteLine("");
                for (int i = cases.Count - 1; i >= 0; i--)//倒序
                {
                    var state = c.GState;
                    string gcase = cases[i];
                    int method = methods[i];
                    Logger.WriteLine(gcase + " " + method.ToString());


                    switch (state)
                    {
                        case GState.Playing:
                            records.Add(gcase, method, CaseResult.Loop);
                            break;
                        case GState.AWin:
                            throw new Exception("不对劲");
                        case GState.BWin:
                            //倒数第一个胜利
                            //倒数第二个失败
                            //倒数第三个胜利
                            //....
                            int j = cases.Count - 1 - i;
                            if (j % 2 == 0)
                            {
                                records.Add(gcase, method, CaseResult.Win);
                            }
                            else
                            {
                                records.Add(gcase, method, CaseResult.Fail);
                            }
                            break;
                    }
                }
                Logger.WriteLine();
            }
            Logger.IsEnabled = true;

            Logger.Clear("data.txt");
            Logger.WriteLine(records, "data.txt");
            //Console.WriteLine(records);

        }

        private static void LoadTrainData(string path = "data.txt")
        {
            string s = File.ReadAllText(path);
            records = Records.LoadFromText(s);
        }
        /// <summary>
        /// 
        /// </summary>
        private static void Play()
        {
            GCase gCase = new GCase();

            AIPLayer aIPLayer = new AIPLayer(Records.LoadFromText(File.ReadAllText(("randomData.txt"))));
            HumanPlayer humanPlayer = new HumanPlayer();

            for (int i = 0; i < 100; i++)
            {
                int method;
                if (i % 2 == 0)
                {
                    Console.WriteLine("--- humanPlayer ---");
                    method = humanPlayer.GetAddMethod(gCase.Nums);
                    Console.WriteLine("--- end ---");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("--- AIPLayer ---");
                    method = aIPLayer.GetAddMethod(gCase.Nums);
                    Console.WriteLine("--- end ---");
                    Console.WriteLine();
                }
                gCase.RunMethod(method);
                gCase.Reserve();
            }

            //RandomPlayer randomPlayer = new RandomPlayer();

        }

#pragma warning disable IDE0060 // 删除未使用的参数
        static void Main(string[] args)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            try
            {
                LoadTrainData();
            }
            catch (Exception)
            {
            }

            Play();
            //Timing(RandomTrain);
        }

    }
}
