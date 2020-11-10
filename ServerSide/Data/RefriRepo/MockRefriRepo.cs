using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorServerSide.Data.RefriRepo
{
    public class MockRefriRepo : BaseRefriRepo
    {
        /// <summary>
        /// Список сенсоров.
        /// </summary>
        private IList<SensorData> _sensors;

        /// <summary>
        /// Список блококв холодильника.
        /// </summary>
        private IList<RefrigeratorBlock> _refriBlocks;

        /// <summary>
        /// Список блококв холодильника.
        /// </summary>
        private IList<Refrigerator> _refrigerators;

        public MockRefriRepo(RefrigeratorContext context, IMapper mapper) : base(context, mapper)
        {
            InitRefrigerators();
            InitRefriBlocks();
            InitSensorData();

            this._context.Refrigerator.AddRange(_refrigerators);
            this._context.RefrigeratorBlock.AddRange(_refriBlocks);
            this._context.SensorData.AddRange(_sensors);
        }

        /// <summary>
        /// Инициализация данных для блоков холодильника.
        /// </summary>
        private void InitRefrigerators()
        {
            _refrigerators = new List<Refrigerator>()
            {
                new Refrigerator()
                {
                    RefrigeratorUUID=this._encrypt.GetSHA512("1"),
                    Name="LG",
                    Description="Стильный, модный, молодежный"
                }
            };
        }

        /// <summary>
        /// Инициализация данных для блоков холодильника.
        /// </summary>
        private void InitRefriBlocks()
        {
            _refriBlocks = new List<RefrigeratorBlock>()
            {
                new RefrigeratorBlock()
                {
                    BlockUUID=this._encrypt.GetSHA512("1"),
                    Name="Блок с яйцами",
                    Refrigerator=_refrigerators[0]
                },
                new RefrigeratorBlock()
                {
                    BlockUUID=this._encrypt.GetSHA512("2"),
                    Name="Блок с водой",
                    Refrigerator=_refrigerators[0]
                }
            };
        }

        /// <summary>
        /// Инициализация данных для сенсоров.
        /// </summary>
        private void InitSensorData()
        {
            _sensors = new List<SensorData>()
            {
                new SensorData() { 
                    SensorUUID=this._encrypt.GetSHA512("1") , 
                    Name="Яйца", 
                    Value="10 шт.",
                    RefrigeratorBlock=_refriBlocks[0]
                },
                 new SensorData() {
                    SensorUUID=this._encrypt.GetSHA512("2") ,
                    Name="Яйца",
                    Value="5 шт.",
                    RefrigeratorBlock=_refriBlocks[0]
                },
                new SensorData() {
                    SensorUUID=this._encrypt.GetSHA512("3") ,
                    Name="Pepsi",
                    Value="2 л.",
                    RefrigeratorBlock=_refriBlocks[1]
                },
                new SensorData() {
                    SensorUUID=this._encrypt.GetSHA512("4") ,
                    Name="Pepsi",
                    Value="1 л.",
                    RefrigeratorBlock=_refriBlocks[1]
                }
            };
        }
    }
}
