using System.Numerics;
using System.Collections.Generic;
using Refrigerator.Models;

namespace Refrigerator.Data 
{
    public interface IPlaceRepo
    {
        /// <summary>
        /// Возвращает словарь {key: LocationName, value: Place} с информацией по каждому месту в холодильнике.
        /// </summary>
        KeyValuePair<string, Place> GetPlaceRefrigerator();
    }
}