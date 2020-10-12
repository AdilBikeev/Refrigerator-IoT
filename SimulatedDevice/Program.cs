using System;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using RefrigeratorRemote.Models;
using Tools;
using SimulatedDevice.Mock;
using SimulatedDevice.Models;
using Newtonsoft.Json.Linq;

namespace SimulatedDevice
{
    class SimulatedDevice
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("IoT Hub Refrigerator - Simulated device. Ctrl-C to exit.\n");

            Startup startup = new Startup();

            Console.ReadLine();
        }
    }
}
