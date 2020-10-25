using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProvider.Interfaces
{
    public interface IAzureIOTClient
    {
        /// <summary>
        /// Метод для отправки данных на Azure IoT.
        /// </summary>
        public void SendDeviceToCloudMessagesAsync(BaseSensorData sensorData);
    }
}
