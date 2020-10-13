using RemoteProvider.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CronJobAzureIoT
{
    class Program
    {
        private static IAzureIOTServer agent = new AzureIOTAgent();

        static async Task Main(string[] args)
        {
            Console.WriteLine($"{nameof(CronJobAzureIoT)}  Read device to cloud messages. Ctrl-C to exit.\n");

            using var cancellationSource = new CancellationTokenSource();

            void cancelKeyPressHandler(object sender, ConsoleCancelEventArgs eventArgs)
            {
                eventArgs.Cancel = true;
                cancellationSource.Cancel();
                Console.WriteLine("Exiting...");

                Console.CancelKeyPress -= cancelKeyPressHandler;
            }

            Console.CancelKeyPress += cancelKeyPressHandler;
            await agent.ReceiveMessagesFromDeviceAsync(cancellationSource.Token);
        }
    }
}
