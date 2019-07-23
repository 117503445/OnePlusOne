using System;

namespace OnePlusOne
{
    public class RandomPlayer : Player
    {
        private static readonly Random random = new Random();
        public override int GetAddMethod(int[] nums)
        {
            if (nums.Length != 4)
            {
                throw new Exception("nums长度不为4");
            }

            while (true)
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
