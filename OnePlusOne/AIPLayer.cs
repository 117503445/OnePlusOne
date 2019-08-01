using System;

namespace OnePlusOne
{
    partial class Program
    {
        public class AIPLayer : Player
        {
            private static readonly Random random = new Random();

            public Records Records;

            public AIPLayer(Records records)
            {
                Records = records ?? throw new ArgumentNullException(nameof(records));
            }

            public override int GetAddMethod(int[] nums)
            {
                if (nums.Length != 4)
                {
                    throw new Exception("nums长度不为4");
                }
                string gcase = "";
                foreach (var num in nums)
                {
                    gcase += num.ToString();
                }
                int method = -1;//最大胜率对应的方法
                foreach (var record in Records.rs)
                {
                    if (record.GCase == gcase)
                    {
                        Console.WriteLine(record.ToString());


                        double maxWinPercentage = 0;//4种方法中的最大胜率

                        for (int i = 0; i < 4; i++)
                        {
                            int sum = 0;
                            for (int j = 0; j < 3; j++)
                            {
                                sum += record.Nums[i * 3 + j];
                            }
                            if (sum != 0)
                            {
                                double winPercentage =(double) record.Nums[i * 3] / sum;
                                Console.WriteLine($"method{i} {winPercentage}");
                                if (winPercentage > maxWinPercentage)
                                {
                                    maxWinPercentage = winPercentage;
                                    method = i;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine(method);
                if (method != -1)
                {
                    return method;
                }


                while (true)//无法确定时,随机选择
                {
                    int r = random.Next(0, 4);
                    if (GCase.IsVaildAddMethod(nums, r))
                    {
                        return r;
                    }
                }
            }

        }

    }
}
