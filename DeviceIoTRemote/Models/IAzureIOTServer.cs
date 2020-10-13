using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteProvider.Models
{
    public interface IAzureIOTServer
    {
        /// <summary>
        /// Метод для чтения сообщения из реестра Azure IoT.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ReceiveMessagesFromDeviceAsync(CancellationToken cancellationToken);
    }
}
