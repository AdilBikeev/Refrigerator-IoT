using RefrigeratorRemote.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedDevice.Models
{
    public abstract class BaseSensorData
    {
        /// <summary>
        /// Объект сенсора на блоке холодильника.
        /// </summary>
        public SensorData SensorData { get; protected set; }
    }
}
