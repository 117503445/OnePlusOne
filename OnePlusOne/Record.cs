using System;

namespace OnePlusOne
{
    public class Record
    {
        private string gCase = "1111";
        public string GCase
        {
            get { return gCase; }
            set
            {
                if (value.Length != 4)
                {
                    throw new Exception("不合法的值");
                }
                gCase = value;
            }
        }
        /// <summary>
        /// 长度为12的int数组,0-2为 Add Method 0 的 胜 负 和 的数量
        /// </summary>
        public int[] Nums { get; } = new int[12];
        public override string ToString()
        {
            string s = GCase + "\t";
            foreach (var num in Nums)
            {
                s += num.ToString() + "\t";
            }
            return s;
        }
        public static Record LoadFromText(string s)
        {
            var strings = s.Split('\t');
            Record record = new Record
            {
                GCase = strings[0]
            };
            for (int i = 0; i < 12; i++)
            {
                record.Nums[i] = int.Parse(strings[i + 1]);
            }
            return record;
        }
    }
}
