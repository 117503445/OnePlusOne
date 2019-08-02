using System;

namespace OnePlusOne
{
    /// <summary>
    /// 一局游戏
    /// </summary>
    public class Game
    {
        /// <summary>
        /// 当前的局面
        /// </summary>
        private readonly GCase GCase = new GCase();
        /// <summary>
        /// 2个玩家
        /// </summary>
        private readonly Player[] Players;
        private readonly int maxStep;
        public Game(Player[] players, int maxStep = 1000)
        {
            Players = players ?? throw new ArgumentNullException(nameof(players));
            if (players.Length != 2)
            {
                throw new ArgumentException("玩家的数量不是2");
            }
            foreach (var player in players)
            {
                if (player == null)
                {
                    throw new ArgumentNullException(nameof(player));
                }
            }
            this.maxStep = maxStep;
        }

        public void Start()
        {
            for (int i = 0; i < maxStep; i++)
            {
                if (GCase.GState != GState.Playing)
                {
                    if (i % 2 == 0)
                    {
                        Console.WriteLine("Player1 Win");
                    }
                    else
                    {
                        Console.WriteLine("Player0 Win");
                    }
                    break;
                }
                int method;
                if (i % 2 == 0)
                {
                    Console.WriteLine("--- Player0 ---");
                    method = Players[0].GetAddMethod(GCase.Nums);
                    Console.WriteLine("--- end ---");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("--- PLayer1 ---");
                    method = Players[1].GetAddMethod(GCase.Nums);
                    Console.WriteLine("--- end ---");
                    Console.WriteLine();
                }
                GCase.RunMethod(method);
                GCase.Reserve();
            }
        }
    }
}
