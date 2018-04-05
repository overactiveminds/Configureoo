using System;
using Configureoo.Core;

namespace Configureoo
{
    public class ConsoleLog : ILog
    {
        public void Debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
        }
    }
}
