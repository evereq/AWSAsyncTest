using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Evereq.AWSAsyncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Starting Normal...");

            DeployCommand();

            Console.Out.WriteLine("Starting Async...");

            AsyncContext.Run(() => DeployCommandAsync());

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static async Task DeployCommandAsync()
        {            
            var deployCommand = new DeployCommand();          
            await deployCommand.StartAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        static void DeployCommand()
        {
            var deployCommand = new DeployCommand();
            deployCommand.Start();
        }
    }
}
