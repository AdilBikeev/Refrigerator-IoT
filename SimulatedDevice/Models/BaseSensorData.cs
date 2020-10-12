using RefrigeratorRemote.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedDevice.Models
{
    public abstract class BaseSensorData : IPropertyUpdateChanged
    {
        /// <summary>
        /// Объект сенсора на блоке холодильника.
        /// </summary>
        public SensorData SensorData { get; protected set; }

        public abstract void Update();
    }
}
