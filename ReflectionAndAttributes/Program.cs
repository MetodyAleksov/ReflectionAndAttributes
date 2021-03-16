using System;

namespace Stealer
{
    class Program
    {
        static void Main(string[] args)
        {
            Hacker hacker = new Hacker();
            Spy spy = new Spy();
            Console.WriteLine(spy.StealFieldInfo("Stealer.Hacker", "username", "password"));
        }
    }
}
