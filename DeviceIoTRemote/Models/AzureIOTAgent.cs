using Microsoft.Azure.Devices.Client;
using Azure.Messaging.EventHubs.Consumer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading;
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

        // Event Hub-compatible endpoint
        // az iot hub show --query properties.eventHubEndpoints.events.endpoint --name {your IoT Hub name}
        private const string EventHubsCompatibleEndpoint = "sb://iothub-ns-refrigerat-5009613-1d5e48ae28.servicebus.windows.net/";

        // Event Hub-compatible name
        // az iot hub show --query properties.eventHubEndpoints.events.path --name {your IoT Hub name}
        private const string EventHubName = "refrigerator";

        // az iot hub policy show --name service --query primaryKey --hub-name {your IoT Hub name}
        private const string IotHubSasKeyName = "service";
        private const string IotHubSasKey = "SeST5mFbdK3284heuL1Tkzi7b35WbGzieuWJDXohcBA=";

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

        private static string BuildEventHubsConnectionString(string eventHubsEndpoint,
                                                     string iotHubSharedKeyName,
                                                     string iotHubSharedKey) =>
    $"Endpoint={ eventHubsEndpoint };SharedAccessKeyName={ iotHubSharedKeyName };SharedAccessKey={ iotHubSharedKey }";

        /// <summary>
        /// Метод для чтения сообщения из реестра Azure IoT.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SensorData> ReceiveMessagesFromDeviceAsync()
        {
            string connectionString = BuildEventHubsConnectionString(EventHubsCompatibleEndpoint, IotHubSasKeyName, IotHubSasKey);

            await using EventHubConsumerClient consumer = new EventHubConsumerClient(EventHubConsumerClient.DefaultConsumerGroupName, connectionString, EventHubName);

            Console.WriteLine("Listening for messages on all partitions");

            try
            {
                await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync())
                {
                    Console.WriteLine("Message received on partition {0}:", partitionEvent.Partition.PartitionId);

                    // Переданное IoT устройством сообщение
                    string data = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
                    Console.WriteLine("\t{0}:", data);

                    return JsonConvert.DeserializeObject<SensorData>(data);
                }
            }
            catch (TaskCanceledException)
            {
                // This is expected when the token is signaled; it should not be considered an
                // error in this scenario.
            }

            return null;
        }
    }
}
