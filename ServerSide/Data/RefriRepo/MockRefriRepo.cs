using AutoMapper;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using RefrigeratorServerSide.Dtos;
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

        public MockRefriRepo(IMapper mapper) : base(mapper)
        {
            InitRefrigerators();
            InitRefriBlocks();
            InitSensorData();
        }

        #region Refrigerator
        public override void CreateRefrigerator(Refrigerator refrigerator)
        {
            if (!_refrigerators.Contains(refrigerator))
            {
                refrigerator.RefrigeratorUUID = _encrypt.GetSHA512(_refrigerators.Count().ToString());
                _refrigerators.Add(refrigerator);
            }
            else
            {
                throw new Exception("Холодильник с указанными данными уже существует !");
            }
        }

        public override IList<RefriReeadDto> GetAllRefri() => this._refrigerators
                                                                  .Select(item => _mapper.Map<RefriReeadDto>(item))
                                                                  .ToList();

        public override Refrigerator GetRefrigerator(string refrigeratorUUID) => GetRefriItem(this._refrigerators.ToList(), refrigeratorUUID);

        public override IList<string> GetRefrigeratorBlocksUUID(string refrigeratorUUID)=> GetRefrigeratorBlocksUUID(this._refriBlocks, refrigeratorUUID);

        public override void UpdateRefriData(RefriReeadDto refrigerator, string refrigeratorUUID) => UpdateRefriData(this._refrigerators.ToList(), refrigerator, refrigeratorUUID);
        #endregion

        #region RegrigeratorBlocks
        public override RefrigeratorBlock GetRefriBlock(string blockUUID) => GetRefriItem(this._refriBlocks, blockUUID);

        public override IList<string> GetRefriBlockSensorsUUID(string blockUUID) => this._sensors.Where(
            sensor => sensor.RefrigeratorBlock
                          .BlockUUID.Equals(blockUUID)
        )
        .Select(sensor => sensor.SensorUUID)
        .ToList();

        public override void UpdBlocksRefriData(IList<string> blocksUUID, Refrigerator refrigerator) =>
            this._refriBlocks
                .Where(block => blocksUUID.Contains(block.BlockUUID))
                .ForAll(block => block.Refrigerator = refrigerator);

        public override void CreateRefriBlock(RefrigeratorBlock refriBlock, out string blockUUID)
        {
            if (!_refriBlocks.Contains(refriBlock))
            {
                blockUUID = _encrypt.GetSHA512(_refriBlocks.Count().ToString());
                refriBlock.BlockUUID = blockUUID;
                _refriBlocks.Add(refriBlock);
            }
            else
            {
                throw new Exception("Блок холодильника с указанными данными уже существует !");
            }
        }

        public override void UpdateRefriBlockData(RefrigeratorBlock refriBlock)
        {
            var blockModel = _refriBlocks
            .FirstOrDefault(item =>
                item.BlockUUID.Equals(refriBlock.BlockUUID)
             );


            if (blockModel is not null)
            {
                blockModel = refriBlock;
            }
            else
            {
                throw new Exception("Блок холодильника с указанными данными не существует !");
            }
        }

        public override void UpdSensorsData(IList<string> sensorsIDS, RefrigeratorBlock refriBlock) =>
            this._sensors
                .Where(sensor => sensorsIDS.Contains(sensor.SensorUUID))
                .ForAll(sensor => sensor.RefrigeratorBlock = refriBlock);
        #endregion

        #region SensorData
        public override void CreateSensor(SensorData sensor, out string sensorUUID)
        {
            if (!_sensors.Contains(sensor))
            {
                sensorUUID = _encrypt.GetSHA512((_sensors.Count() + 1).ToString());
                sensor.SensorUUID = sensorUUID;
                _sensors.Add(sensor);
            }
            else
            {
                throw new Exception("Холодильник с указанными данными уже существует !");
            }
        }

        public override SensorData GetSensor(string sensorUUID) => GetRefriItem(this._sensors, sensorUUID);

        public override bool SaveChanges() => true;
        #endregion
    }
}
