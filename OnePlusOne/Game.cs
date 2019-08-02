using System;

namespace OnePlusOne
{
    /// <summary>
    /// 一局游戏
    /// </summary>
    public class Game
    {
        /// <summary>
        /// 是否启用游戏日志
        /// </summary>
        public bool IsEnabledGameLog { get; set; } = true;
        /// <summary>
        /// 输出游戏日志
        /// </summary>
        /// <param name="o"></param>
        private void GameLog(object o = null)
        {
            if (IsEnabledGameLog)
            {
                if (o == null)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(o);
                }
            }
        }
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
                        GameLog("Player1 Win");
                    }
                    else
                    {
                        GameLog("Player0 Win");
                    }
                    break;
                }
                int method;
                if (i % 2 == 0)
                {
                    GameLog("--- Player0 ---");
                    method = Players[0].GetAddMethod(GCase.Nums);
                    GameLog("--- end ---");
                    GameLog();
                }
                else
                {
                    GameLog("--- PLayer1 ---");
                    method = Players[1].GetAddMethod(GCase.Nums);
                    GameLog("--- end ---");
                    GameLog();
                }
                GameLog("------");
                GameLog(GCase);
                GameLog(method);
                GCase.RunMethod(method);
                GameLog(GCase);
                GCase.Reserve();
                GameLog(GCase);
                GameLog("------");
                GameLog();
            }
        }
    }
}
