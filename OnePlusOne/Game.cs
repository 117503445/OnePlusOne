using System;
using System.Collections.Generic;
using System.IO;

namespace OnePlusOne
{
    /// <summary>
    /// 一局游戏
    /// </summary>
    public class Game
    {
        /// <summary>
        /// 已经进行的步数
        /// </summary>
        public int Step { get; private set; } = 0;
        /// <summary>
        /// 游戏状态
        /// </summary>
        public GState GState
        {
            get; private set;
        } = GState.Playing;

        /// <summary>
        /// 是否启用游戏日志
        /// </summary>
        public bool IsEnabledGameLog { get; set; } = true;
        private bool IsTrainMode { get { return Records != null; } }
        public Records Records;
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
        public Game(Player[] players, int maxStep = 120)
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

            List<string> cases = new List<string>();
            List<int> methods = new List<int>();

            for (Step = 0; Step < maxStep; Step++)
            {
                if (GCase.GState != GState.Playing)
                {
                    if (Step % 2 == 0)
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
                if (Step % 2 == 0)
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

                if (IsTrainMode)
                {
                    cases.Add(GCase.ToString());
                    methods.Add(method);
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
            if (IsTrainMode)
            {
                for (int i = cases.Count - 1; i >= 0; i--)//倒序
                {
                    var state = GCase.GState;
                    string gcase = cases[i];
                    int method = methods[i];
                    switch (state)
                    {
                        case GState.Playing:
                            Records.Add(gcase, method, CaseResult.Loop);
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
                                Records.Add(gcase, method, CaseResult.Win);
                            }
                            else
                            {
                                Records.Add(gcase, method, CaseResult.Fail);
                            }
                            break;
                    }
                }
            }
        }
    }
}
