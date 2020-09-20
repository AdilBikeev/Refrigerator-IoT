using System.Collections.Generic;
using Refrigerator.Models;

namespace Refrigerator.Data 
{
    public class MockPlaceRepo : IPlaceRepo
    {
        /// <summary>
        /// Названия мест в холодильнике
        /// </summary>
        enum PlaceName
        {
            Top,
            Middle,
            Bottom
        }

        /// <summary>
        /// Типы пищи.
        /// </summary>
        enum FoodType
        {
            Water,
            Meat,
            Fruit
        }

        public Dictionary<string, Place> GetPlaceRefrigerator()
        {
            return new Dictionary<string, Place>
            {
                [nameof(PlaceName.Top)] = new Place { placeId = 1, name = nameof(PlaceName.Top), locationId = (int)PlaceName.Top, pressure = 0, foodType = nameof(FoodType.Water) },
                [nameof(PlaceName.Middle)] = new Place { placeId = 1, name = nameof(PlaceName.Middle), locationId = (int)PlaceName.Middle, pressure = 0.5f, foodType = nameof(FoodType.Meat) },
                [nameof(PlaceName.Bottom)] = new Place { placeId = 1, name = nameof(PlaceName.Bottom), locationId = (int)PlaceName.Bottom, pressure = 1, foodType = nameof(FoodType.Fruit) },
            };
        }
    }
}