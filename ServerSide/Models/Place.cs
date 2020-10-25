using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace RefrigeratorServerSide.Models 
{
    /// <summary>
    /// Названия мест в холодильнике
    /// </summary>
    public enum PlaceName
    {
        Top,
        Middle,
        Bottom
    }

    /// <summary>
    /// Типы пищи.
    /// </summary>
    public enum FoodType
    {
        Water,
        Meat,
        Fruit
    }

    /// <summary>
    /// Место в холодильнике.
    /// </summary>
    public class Place
    {
        private string _name;
        private string _foodType;

        [Key]
        /// <summary>
        /// идентификатор места.
        /// </summary>
        public  int placeId { get; set; }

        [Required]
        /// <summary>
        /// Название места.
        /// </summary>
        public string name
        {
            get => this._name;
            set
            {
                if (Enum.GetNames(typeof(PlaceName)).Contains(value))
                {
                    this._name = value;
                }
                else
                {
                    throw new KeyNotFoundException($"Не существует места в холодильнике с названием {value}");
                }
            } 
        }

        [Required]
        /// <summary>
        /// Идентификатор местаположения.
        /// </summary>
        public int locationId { get; set; }

       [Required]
        /// <summary>
        /// Давление
        /// </summary>
        /// <value>From 0 to 1</value>
        public float pressure { get; set; }

        [Required]
        /// <summary>
        /// Тип еды.
        /// </summary>
        public string foodType
        {
            get => this._foodType;
            set
            {
                if (Enum.GetNames(typeof(FoodType)).Contains(value))
                {
                    this._foodType = value;
                }
                else
                {
                    throw new KeyNotFoundException($"Не существует типа еды в холодильнике с названием {value}");
                }
            } 
        }
    }
}