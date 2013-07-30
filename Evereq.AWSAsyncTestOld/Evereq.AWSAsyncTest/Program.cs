using System;

namespace Evereq.AWSAsyncTestOld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Starting Normal...");

            DeployCommand();
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
       
        static void DeployCommand()
        {
            var deployCommand = new DeployCommand();
            deployCommand.Start();
        }
    }
}
