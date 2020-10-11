using System;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using RefrigeratorRemote.Models;
using Tools;
using SimulatedDevice.Mock;
using SimulatedDevice.Models;

namespace SimulatedDevice
{
    class SimulatedDevice
    {
        private static DeviceClient _deviceClient;

        // The device connection string to authenticate the device with your IoT hub.
        // Using the Azure CLI:
        // az iot hub device-identity show-connection-string --hub-name {YourIoTHubName} --device-id MyDotnetDevice --output table
        private readonly static string connectionString = "HostName=Refrigerator.azure-devices.net;DeviceId=RefrigeratorDevice;SharedAccessKey=crmWehLrWmeDBFp9TyXLLoVWJoMbJBxvPtbj7Vw3mA4=";

        /// <summary>
        /// Метод для отправки данных на Azure IoT.
        /// </summary>
        private static async void SendDeviceToCloudMessagesAsync(BaseRefrigerator refrigerator, BaseRefrigeratorBlock refrigeratorBlock)
        {
            while (true)
            {
                // Передаваемое сообщение в Azure IoT
                var messageString = JsonConvert.SerializeObject(refrigerator);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                // Send the telemetry message
                //await _deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(5000);
            }
        }
        private static void Main(string[] args)
        {
            Console.WriteLine("IoT Hub Refrigerator - Simulated device. Ctrl-C to exit.\n");

            // Connect to the IoT hub using the MQTT protocol
            _deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
            //SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }
    }
}
