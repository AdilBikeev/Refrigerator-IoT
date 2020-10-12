using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProvider.Models
{
    /// <summary>
    /// Агент, для общения с AzureIOT.
    /// </summary>
    public class AzureIOTAgent
    {
        private readonly DeviceClient _deviceClient;

        /// <summary>
        /// Интервал в секундах для повторение отправки данных на Azure IoT.
        /// </summary>
        private const int perSeconds= 5;

        // The device connection string to authenticate the device with your IoT hub.
        // Using the Azure CLI:
        // az iot hub device-identity show-connection-string --hub-name {YourIoTHubName} --device-id MyDotnetDevice --output table
        private readonly string connectionString = "HostName=Refrigerator.azure-devices.net;DeviceId=RefrigeratorDevice;SharedAccessKey=crmWehLrWmeDBFp9TyXLLoVWJoMbJBxvPtbj7Vw3mA4=";

        public AzureIOTAgent()
        {
            // Connect to the IoT hub using the MQTT protocol
            this._deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
        }

        /// <summary>
        /// Метод для отправки данных на Azure IoT.
        /// </summary>
        public async void SendDeviceToCloudMessagesAsync(BaseSensorData sensorData)
        {
            while (true)
            {
                // Передаваемое сообщение в Azure IoT
                var messageString = JsonConvert.SerializeObject(sensorData);

                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                #if (DEBUG)
                    Console.WriteLine("DEBUG: {0} > Sending message: {1}", DateTime.Now, JObject.Parse(messageString));
                #else
                    // Send the telemetry message
                    await _deviceClient.SendEventAsync(message);
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, JObject.Parse(messageString));
                #endif

                

                await Task.Delay(perSeconds*1000);

                sensorData.Update();
            }
        }
    }
}
