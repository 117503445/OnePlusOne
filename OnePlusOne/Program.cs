using System;
using System.Collections.Generic;
using System.IO;

namespace OnePlusOne
{
    partial class Program
    {
        /// <summary>
        /// 由 AIPlayer 和 RandomPlayer 进行多次自我随机对弈训练
        /// </summary>
        private static void AITrain()
        {

        }

        /// <summary>
        /// 由1个 RandomPlayer 进行多次自我随机对弈训练
        /// </summary>
        /// <param name="path">训练数据路径</param>
        /// <param name="trainNums">训练次数</param>
        private static void RandomTrain(string path = "randomData.txt", int trainNums = 50000)
        {
            Records records;
            if (File.Exists(path))
            {
                records = Records.LoadFromText(File.ReadAllText(path));
            }
            else
            {
                records = new Records();
            }
            Logger.Clear();
            Logger.IsEnabled = false;
            
            int maxStep = 120;//单次训练允许的最大回合,一般1000次训练中单词最大回合数小于100
            Console.CursorVisible = false;
            for (int trainIndex = 0; trainIndex < trainNums; trainIndex++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"{trainIndex + 1}/{trainNums}");

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
            Console.CursorVisible = true;
            Logger.IsEnabled = true;


            Logger.WriteLine(records, path);
        }
        /// <summary>
        /// HumanPlayer 与 AIPLayer 进行博弈
        /// </summary>
        private static void HumanVsAI(string path = "data.txt")
        {
            AIPLayer aIPLayer = new AIPLayer(Records.LoadFromText(File.ReadAllText((path))));
            HumanPlayer humanPlayer = new HumanPlayer();

            Game game = new Game(new Player[] { humanPlayer, aIPLayer });
            game.Start();
        }

#pragma warning disable IDE0060 // 删除未使用的参数
        static void Main(string[] args)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            try
            {
                HumanVsAI();
            }
            catch (Exception)
            {
                Console.WriteLine("LoadTrainData failed");
                Console.WriteLine("Press Enter to random train and ai train");
                Console.ReadLine();

                var beginTime = DateTime.Now;
                RandomTrain();
                var endTime = DateTime.Now;
                Console.WriteLine();
                Console.WriteLine($"Start at {beginTime}\nEnd at   {endTime}\n{(endTime - beginTime).TotalMilliseconds}ms");
                Console.WriteLine();

                Console.WriteLine();
                HumanVsAI();
            }
        }

    }
}
