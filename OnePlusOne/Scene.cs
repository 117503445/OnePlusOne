using System;
using System.Diagnostics;

namespace OnePlusOne
{
    public static class Scene
    {
        /// <summary>
        /// 对 Player 进行胜率测试,用 RandomPlayer 与之对战 n 次
        /// </summary>
        public static void PlayerTest(Player player, int n = 10000)
        {
            RandomPlayer randomPlayer = new RandomPlayer();
            Game game = new Game(new Player[] { randomPlayer, player })
            {
                IsEnabledGameLog = false
            };
            int a = 0, b = 0;
            Console.WriteLine();
            Console.WriteLine("-----");
            Console.WriteLine("Player Testing");
            Console.CursorVisible = false;
            for (int i = 0; i < n; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"{i + 1}/{n}");
                game.Start();
                if (game.GState == GState.AWin)
                {
                    a++;
                }
                else if (game.GState == GState.BWin)
                {
                    b++;
                }
            }
            Console.CursorVisible = true;

            Console.WriteLine();
            Console.WriteLine($"in {n} times, player win {b} times, randomPlayer win {a} times, {(double)b / (a + b) * 100}%");
            Console.WriteLine("-----");
        }
        public static void GodVsHuman()
        {
            GodPlayer godPlayer = new GodPlayer();
            HumanPlayer humanPlayer = new HumanPlayer();
            Game game = new Game(new Player[] { humanPlayer, godPlayer });
            game.Start();
        }
        public static void VSRandomPlayer()
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
        /// <summary>
        /// 测试上帝玩家
        /// </summary>
        public static void TestGodPlayer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            GodPlayer godPlayer = new GodPlayer()
            {
                IsEnabledGameLog = false
            };
            PlayerTest(godPlayer);
            Console.WriteLine(stopwatch.Elapsed);
            stopwatch.Stop();
        }
    }
}
