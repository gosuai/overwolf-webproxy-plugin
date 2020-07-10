using System;
using ai.gosu;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new WebProxy();
            t.Start("http://localhost:9999", "*", "*", "*");
            Console.ReadKey(true);
        }
    }
}
