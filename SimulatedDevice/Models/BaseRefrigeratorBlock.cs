using RefrigeratorRemote.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedDevice.Models
{
    public abstract class BaseRefrigeratorBlock
    {
        /// <summary>
        /// Объект блока холодильника.
        /// </summary>
        public RefrigeratorBlock RefrigeratorBlock { get; protected set; }
    }
}
