using System;

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
