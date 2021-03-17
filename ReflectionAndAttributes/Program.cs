using System;

namespace Stealer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Hacker hacker = new Hacker();
            Spy spy = new Spy();
            Console.WriteLine(spy.AnalyzeAccessModifiers("Hacker"));
        }
    }
}
