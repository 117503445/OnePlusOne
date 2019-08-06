using System;
using System.Collections.Generic;

namespace OnePlusOne
{
    /// <summary>
    /// 一局游戏
    /// </summary>
    public class Game
    {
        /// <summary>
        /// 游戏状态
        /// </summary>
        public GState GState
        {
            get; private set;
        } = GState.Playing;

        public List<KeyValuePair<string, int>> CaseMothodPairs { get; set; } = new List<KeyValuePair<string, int>>();
        /// <summary>
        /// 是否启用游戏日志
        /// </summary>
        public bool IsEnabledGameLog { get; set; } = true;
        private bool IsTrainMode { get { return Records != null; } }
        private Records Records;
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
        private GCase GCase;
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
        /// <summary>
        /// 加载训练数据集,关闭日志输出
        /// </summary>
        /// <param name="records"></param>
        public void LoadTrainRecords(Records records)
        {
            Records = records;
            IsEnabledGameLog = false;
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        public void Start()
        {
            GCase = new GCase();
            for (int step = 0; step < maxStep; step++)
            {
                if (GCase.GState != GState.Playing)
                {
                    if (step % 2 == 0)
                    {
                        GState = GState.BWin;
                        GameLog("Player1 Win");
                    }
                    else
                    {
                        GState = GState.AWin;

                        GameLog("Player0 Win");
                    }
                    break;
                }
                int method;
                if (step % 2 == 0)
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
