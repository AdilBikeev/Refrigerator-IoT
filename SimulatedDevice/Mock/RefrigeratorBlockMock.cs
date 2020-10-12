using RefrigeratorRemote.Models;
using SimulatedDevice.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace SimulatedDevice.Mock
{
    /// <summary>
    /// Объект для Mock данных холодильника.
    /// </summary>
    public class RefrigeratorBlockMock : BaseRefrigeratorBlock
    {
        /// <summary>
        /// Список доступных имен блоков холодильника.
        /// </summary>
        private readonly List<string> blockNames = new List<string> { "Яйца", "Вода", "Морозилка", "Фрукты" };

        protected MockHelper mockHelper = new MockHelper();

        /// <summary>
        /// Объект для шифрования строк.
        /// </summary>
        protected Encrypt encrypt = new Encrypt();

        public RefrigeratorBlockMock(int index, BaseRefrigerator refrigerator)
        {
            this.RefrigeratorBlock = new RefrigeratorBlock
            {
                BlockUUID = encrypt.GetSHA512(index.ToString()),
                Name = this.mockHelper.GetRandomValue(this.blockNames),
                Refrigerator = refrigerator.Refrigerator
            };
        }
    }
}
