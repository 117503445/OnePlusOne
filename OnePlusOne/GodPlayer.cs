using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace OnePlusOne
{
    /// <summary>
    /// 上帝,由 ZLQ 的 C++ 算法作为核心
    /// </summary>
    public class GodPlayer : Player
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
        private readonly string[] success;
        private readonly string[] fail;
        public GodPlayer(string pathSuccess = "C++/success.txt", string pathFail = "C++/fail.txt")
        {
            success = File.ReadAllLines(pathSuccess);
            fail = File.ReadAllLines(pathFail);
        }

        public override int GetAddMethod(int[] nums)
        {
            if (nums.Length != 4)
            {
                throw new ArgumentException();
            }
            Dictionary<int, string> next = new Dictionary<int, string>();
            for (int i = 0; i < 4; i++)
            {
                GCase current = new GCase();
                for (int j = 0; j < 4; j++)
                {
                    current.Nums[j] = nums[j];
                }
                if (GCase.IsVaildAddMethod(nums, i))
                {
                    current.RunMethod(i);
                    current.Reserve();
                    string s = "";
                    foreach (var c in current.Nums)
                    {
                        s += c;
                    }
                    next.Add(i, s);
                }
            }
            foreach (var item in next)
            {
                GameLog(item);
            }
            int result = -1;
            //如果下一步是对手的必败策略,则这一步是自己的必胜策略
            foreach (var item in next)
            {
                string value = item.Value;
                string newValue = $"{value[1]}{value[0]}{value[3]}{value[2]}";

                if (fail.Contains(newValue))
                {
                    GameLog($"Found {item.Key} {item.Value} SUCCESS");
                    result = item.Key;
                    break;
                }
            }
            //如果下一步是对手的必胜策略,则这一步是自己的必败策略
            //如果没有自己的必胜策略,则选择不是必败的策略
            foreach (var item in next)
            {
                string value = item.Value;
                string newValue = $"{value[1]}{value[0]}{value[3]}{value[2]}";

                if (success.Contains(newValue))
                {
                    GameLog($"Found {item.Key} {item.Value} FAIL");
                }
                else
                {
                    result = item.Key;
                    break;
                }
            }
            if (result == -1)
            {
                GameLog("必败!!!");
                result = 0;
            }
            return result;
        }
    }
}
