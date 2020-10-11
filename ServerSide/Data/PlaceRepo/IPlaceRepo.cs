using System.Numerics;
using System.Collections.Generic;
using RefrigeratorServerSide.Models;

namespace RefrigeratorServerSide.Data 
{
    public interface IPlaceRepo
    {
        /// <summary>
        /// Возвращает словарь {key: LocationName, value: Place} с информацией по каждому месту в холодильнике.
        /// </summary>
        Dictionary<string, List<Place>> GetPlaceRefrigerator();
    }
}