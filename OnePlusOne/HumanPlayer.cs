using System;

namespace OnePlusOne
{
    public class HumanPlayer : Player
    {
        public override int GetAddMethod(int[] nums)
        {
            Console.WriteLine($"Your Num is {nums[0]} {nums[1]}");
            Console.WriteLine($"His Num is {nums[2]} {nums[3]}");
            Console.WriteLine("Please Input 0 0+=2 1 1+=2 2 0+=3 3 1+=3");
            while (true)
            {
                string s = Console.ReadLine();
                int.TryParse(s, out int method);
                if (GCase.IsVaildAddMethod(nums, method))
                {
                    return method;
                }
            }
        }
    }
}
