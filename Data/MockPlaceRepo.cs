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

        public Dictionary<string, List<Place>> GetPlaceRefrigerator()
        {
            return new Dictionary<string, List<Place>>
            {
                [nameof(PlaceName.Top)] = new List<Place> {
                    new Place { placeId = 1, name = nameof(PlaceName.Top), locationId = (int)PlaceName.Top, pressure = 0, foodType = nameof(FoodType.Water) },
                    new Place { placeId = 1, name = nameof(PlaceName.Top), locationId = (int)PlaceName.Top, pressure = 0.1f, foodType = nameof(FoodType.Water) },
                    new Place { placeId = 1, name = nameof(PlaceName.Top), locationId = (int)PlaceName.Top, pressure = 0.11f, foodType = nameof(FoodType.Water) },
                },
                [nameof(PlaceName.Middle)] = new List<Place> {
                    new Place { placeId = 2, name = nameof(PlaceName.Middle), locationId = (int)PlaceName.Middle, pressure = 0, foodType = nameof(FoodType.Meat) },
                    new Place { placeId = 2, name = nameof(PlaceName.Middle), locationId = (int)PlaceName.Middle, pressure = 0.2f, foodType = nameof(FoodType.Meat) },
                    new Place { placeId = 2, name = nameof(PlaceName.Middle), locationId = (int)PlaceName.Middle, pressure = 0.22f, foodType = nameof(FoodType.Meat) },
                },
                [nameof(PlaceName.Bottom)] = new List<Place> {
                    new Place { placeId = 3, name = nameof(PlaceName.Bottom), locationId = (int)PlaceName.Bottom, pressure = 0, foodType = nameof(FoodType.Fruit) },
                    new Place { placeId = 3, name = nameof(PlaceName.Bottom), locationId = (int)PlaceName.Bottom, pressure = 0.3f, foodType = nameof(FoodType.Fruit) },
                    new Place { placeId = 3, name = nameof(PlaceName.Bottom), locationId = (int)PlaceName.Bottom, pressure = 0.33f, foodType = nameof(FoodType.Fruit) },
                }
            };
        }
    }
}