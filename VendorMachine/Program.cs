using System;
using VendorMachine.View;

namespace VendorMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new CLI();
            //cli.RunWithStatus();
            cli.Run();
        }
    }
}
