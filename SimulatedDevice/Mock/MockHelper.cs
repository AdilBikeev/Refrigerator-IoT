using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatedDevice.Mock
{
    public class MockHelper
    {
        /// <summary>
        /// Возращает рандомное значение из списка.
        /// </summary>
        /// <typeparam name="T">Тип данных списка.</typeparam>
        /// <param name="lst">Список значений.</param>
        /// <returns></returns>
        public T GetRandomValue<T>(List<T> lstValues) => lstValues[(new Random()).Next(0, lstValues.Count - 1)];
    }
}
