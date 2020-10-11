using RefrigeratorRemote.Models;
using SimulatedDevice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tools;
using Extensions;

namespace SimulatedDevice.Mock
{
    /// <summary>
    /// Объект для Mock данных сенсоров на блоке холодильника.
    /// </summary>
    public class SensorDataMock : BaseSensorData
    {
        enum SensorName
        {
            [Description("Давление")]
            Pressure,

            [Description("Температура")]
            Temperature
        }

        /// <summary>
        /// Список доступных имен сенсоров блока холодильника.
        /// </summary>
        private readonly List<string> sensorNames = new List<string> 
        { 
            SensorName.Pressure.ToName(), 
            SensorName.Temperature.ToName() 
        };

        /// <summary>
        /// Словарь соответствий типов сенсоров их единицы измерения.
        /// </summary>
        private Dictionary<string, string> dictDataTypes = new Dictionary<string, string>
        {
            [SensorName.Pressure.ToName()] = "Па",
            [SensorName.Temperature.ToName()] = "℃",
        };

        protected MockHelper mockHelper = new MockHelper();

        /// <summary>
        /// Объект для шифрования строк.
        /// </summary>
        protected Encrypt encrypt = new Encrypt();

        public SensorDataMock(int index, Refrigerator refrigerator)
        {
            var sensorName = this.mockHelper.GetRandomValue(this.sensorNames);
            this.SensorData = new SensorData
            {
                SensorUUID = encrypt.GetSHA512(index.ToString()),
                Name = this.mockHelper.GetRandomValue(this.sensorNames),
                Value = $"{(new Random().Next(0, 35))} {this.dictDataTypes[sensorName]}"
            };
        }
    }
}
