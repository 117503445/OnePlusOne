using System;
using System.IO;

namespace OnePlusOne
{
    public static class Logger
    {
        private static readonly string logPath = "log.txt";
        public static bool IsEnabled { get; set; } = true;
        public static void WriteLine(object o = null, string path = "")
        {
            if (!IsEnabled)
            {
                return;
            }
            if (path == "")
            {
                path = logPath;
            }
            if (o != null)
            {
                File.AppendAllText(path, o.ToString() + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(path, Environment.NewLine);
            }
        }
        public static void Clear(string path = "")
        {
            if (path == "")
            {
                path = logPath;
            }

            File.WriteAllText(path, "");
        }
    }
}
