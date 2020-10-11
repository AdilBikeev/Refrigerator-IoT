using RefrigeratorRemote.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedDevice.Models
{
    public abstract class BaseRefrigerator
    {
        /// <summary>
        /// Объект холодильника.
        /// </summary>
        public Refrigerator Refrigerator { get; protected set; }
    }
}
