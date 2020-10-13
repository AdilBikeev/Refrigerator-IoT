using RemoteProvider.Models;
using SimulatedDevice.Mock;
using System.Collections.Generic;

namespace SimulatedDevice
{
    public class Startup
    {
        /// <summary>
        /// Кол-во холодильников.
        /// </summary>
        private const int countRefri = 1;

        /// <summary>
        /// Кол-во блоков в холодильниках.
        /// </summary>
        private const int countRefriBLock = 1;

        /// <summary>
        /// Кол-во сенсоров на блоке холодильника.
        /// </summary>
        private const int countsensorData = 1;

        private List<BaseRefrigerator> refrigerators = new List<BaseRefrigerator>();
        private List<BaseRefrigeratorBlock> refrigeratorBlocks = new List<BaseRefrigeratorBlock>();
        private List<BaseSensorData> sensorDatas = new List<BaseSensorData>();

        private AzureIOTAgent agent = new AzureIOTAgent();

        public Startup()
        {
            for (int i = 0; i < countRefri; i++)
            {
                this.refrigerators.Add(new RefrigeratorMock(i));

                for (int j = 0; j < countRefriBLock; j++)
                {
                    this.refrigeratorBlocks.Add(new RefrigeratorBlockMock(j, this.refrigerators[i]));

                    for (int k = 0; k < countsensorData; k++)
                    {
                        this.sensorDatas.Add(new SensorDataMock(k, this.refrigeratorBlocks[j]));
                        this.agent.SendDeviceToCloudMessagesAsync(this.sensorDatas[k]);
                    }
                }
            }
        }
    }
}
