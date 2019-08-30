using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnePlusOne
{
    public static class Scene
    {
        /// <summary>
        /// 对 Player 进行胜率测试,用 RandomPlayer 与之对战 n 次
        /// </summary>
        public static void PlayerTest(Player player, int n = 10000)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            RandomPlayer randomPlayer = new RandomPlayer();

            int a = 0, b = 0, c = 0;
            Console.WriteLine();
            Console.WriteLine("-----");
            Console.WriteLine("Player Testing");
            object oLock = new object();
            Parallel.For(0, n, (i) =>
            {
                Game game = new Game(new Player[] { randomPlayer, player })
                {
                    IsEnabledGameLog = false
                };
                game.Start();
                lock (oLock)
                {
                    switch (game.GState)
                    {
                        case GState.Playing:
                            c++;
                            break;
                        case GState.AWin:
                            a++;
                            break;
                        case GState.BWin:
                            b++;
                            break;
                        default:
                            break;
                    }
                }

            });
            Console.WriteLine();
            Console.WriteLine($"in {n} times, player win {b} times, randomPlayer win {a} times, peace {c} times,{(double)b / (a + b) * 100}%");
            Console.WriteLine("-----");

            Console.WriteLine(stopwatch.Elapsed);
            stopwatch.Stop();
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
