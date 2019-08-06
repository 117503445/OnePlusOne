using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OnePlusOne
{
    public enum CaseResult
    {
        Win,
        Fail,
        Loop
    }
    public class Records
    {
        public readonly List<Record> rs = new List<Record>();
        public override string ToString()
        {
            string s = "";
            foreach (var record in rs)
            {
                s += record + Environment.NewLine;
            }
            return s;
        }
        private void Sort()
        {
            rs.Sort(new RecordComparer());
        }
        /// <summary>
        /// 增加一次
        /// </summary>
        /// <param name="gcase"></param>
        /// <param name="addMethod"></param>
        /// <param name="result"></param>
        public void Add(string gcase, int addMethod, CaseResult result)
        {
            if (gcase.Length != 4)
            {
                throw new Exception("不合法的值");
            }
            if (addMethod < 0 || addMethod > 3)
            {
                throw new Exception("不合法的值");
            }
            bool isFound = false;
            foreach (var record in rs)
            {
                if (record.GCase == gcase)
                {
                    isFound = true;
                    int index = addMethod * 3 + (int)result;
                    record.Nums[index]++;
                    break;
                }
            }
            if (!isFound)
            {
                Record record = new Record() { GCase = gcase };
                int index = addMethod * 3 + (int)result;
                record.Nums[index]++;
                rs.Add(record);
                Sort();
            }
        }
        /// <summary>
        /// 插入1条记录
        /// </summary>
        /// <param name="record"></param>
        public void Insert(Record record)
        {
            rs.Add(record);
        }
        /// <summary>
        /// 从序列化的字符串中加载
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Records LoadFromText(string s)
        {
            Records records = new Records();
            var lines = s.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                if (line.Length != 0)
                {
                    records.Insert(Record.LoadFromText(line));
                }
            }
            return records;
        }
    }
    public class RecordComparer : IComparer<Record>
    {
        public int Compare(Record x, Record y)
        {
            return int.Parse(x.GCase) - int.Parse(y.GCase);
        }
    }
}
