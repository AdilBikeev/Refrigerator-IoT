using SimulatedDevice.Mock;
using SimulatedDevice.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        private const int countRefriBLock = 5;

        /// <summary>
        /// Кол-во сенсоров на блоке холодильника.
        /// </summary>
        private const int countsensorData = 5;

        private List<BaseRefrigerator> refrigerators;
        private List<BaseRefrigeratorBlock> refrigeratorBlocks;
        private List<BaseSensorData> sensorDatas;

        public Startup()
        {
            for (int i = 0; i < countRefri; i++)
            {
                this.refrigerators.Add(new RefrigeratorMock(i));

                for (int j = 0; j < 3; j++)
                {
                    this.refrigeratorBlocks.Add(new RefrigeratorBlockMock(j, this.refrigerators[i]));

                    for (int k = 0; k < 5; k++)
                    {
                        this.sensorDatas.Add(new SensorDataMock(k, this.refrigeratorBlocks[j]));
                    }
                }
            }
        }
    }
}
