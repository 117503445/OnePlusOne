using System;
using System.Diagnostics;

namespace OnePlusOne
{
    partial class Program
    {

#pragma warning disable IDE0060 // 删除未使用的参数
        private static void Main(string[] args)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            Scene.PlayerTest(new GodPlayer() { IsEnabledGameLog = false }, 1000000);
            //Scene.GodVsHuman();
        }

    }
}
