using System;

namespace OnePlusOne
{
    public class GCase
    {
        /// <summary>
        /// 前2个,后2个数字分别是2个玩家的数字
        /// </summary>
        public int[] Nums { get; } = new int[] { 1, 1, 1, 1 };
        public GState GState
        {
            get
            {
                if (Nums[0] == 0 && Nums[1] == 0)
                {
                    return GState.AWin;
                }
                if (Nums[2] == 0 && Nums[3] == 0)
                {
                    return GState.BWin;
                }
                return GState.Playing;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method">0:2加到0 1:2加到1 2:3加到0 3:3加到1</param>
        public void Add(int method)
        {
            if (!IsVaildAddMethod(Nums, method))
            {
                throw new Exception("不合法的加法");
            }
            switch (method)
            {
                case 0:
                    AddHelp(0, 2);
                    break;
                case 1:
                    AddHelp(1, 2);
                    break;
                case 2:
                    AddHelp(0, 3);
                    break;
                case 3:
                    AddHelp(1, 3);
                    break;
                default:
                    throw new Exception("method不合法");
            }
        }
        public static bool IsVaildAddMethod(int[] nums, int method)
        {
            if (nums.Length != 4)
            {
                return false;
                //throw new Exception("nums长度不为4");
            }
            switch (method)
            {
                case 0:
                    if (nums[0] == 0 || nums[2] == 0)
                    {
                        return false;
                    }
                    break;
                case 1:
                    if (nums[1] == 0 || nums[2] == 0)
                    {
                        return false;
                    }
                    break;
                case 2:
                    if (nums[0] == 0 || nums[3] == 0)
                    {
                        return false;
                    }
                    break;
                case 3:
                    if (nums[1] == 0 || nums[3] == 0)
                    {
                        return false;
                    }
                    break;
                default:
                    //throw new Exception("method不合法");
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Nums[i] += Nums[j];
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void AddHelp(int i, int j)
        {
            Nums[i] += Nums[j];
            Nums[i] %= 10;
        }
        public void Format()
        {
            if (Nums[0] < Nums[1])
            {
                CSharp.Swap(ref Nums[0], ref Nums[1]);
            }
            if (Nums[2] < Nums[3])
            {
                CSharp.Swap(ref Nums[2], ref Nums[3]);
            }
        }
        /// <summary>
        ///交换双方的数字,并自动整理
        /// </summary>
        public void Reserve()
        {
            CSharp.Swap(ref Nums[0], ref Nums[2]);
            CSharp.Swap(ref Nums[1], ref Nums[3]);
            Format();
        }
        public void WriteLine()
        {
            foreach (var num in Nums)
            {
                Console.WriteLine(num);
            }
        }
        public override string ToString()
        {
            string s = "";
            foreach (var num in Nums)
            {
                s += num.ToString();
            }
            return s;
        }
    }
}
