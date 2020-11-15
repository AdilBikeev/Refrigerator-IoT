using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RefrigeratorServerSide.Dtos;
using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace RefrigeratorServerSide.Data.RefriRepo
{
    /// <summary>
    /// Класс для взаимодействия с БД холодильника с помощью EF Core.
    /// </summary>
    public class SqlReftiRepo : BaseRefriRepo
    {
        private readonly RefrigeratorContext _context;
        public override bool SaveChanges() => this._context.SaveChanges() >= 0;

        public SqlReftiRepo(RefrigeratorContext context, IMapper mapper) : base(mapper) => (_context) = (context);

        #region Refrigerator
        public override void CreateRefrigerator(Refrigerator refrigerator)
        {
            if (!_context.Refrigerator.Contains(refrigerator))
            {
                refrigerator.RefrigeratorUUID = _encrypt.GetSHA512(_context.Refrigerator.Count().ToString());
                _context.Refrigerator.Add(refrigerator);
            }
            else
            {
                throw new Exception("Холодильник с указанными данными уже существует !");
            }
        }

        public override IList<Refrigerator> GetAllRefri() => this._context.Refrigerator.ToList();

        public override Refrigerator GetRefrigerator(string refrigeratorUUID) => GetRefriItem(this._context
        .Refrigerator.ToList(), refrigeratorUUID);

        public override IList<RefrigeratorBlock> GetAllRefriBlock() => this._context.RefrigeratorBlock.ToList();

        public override IList<string> GetRefrigeratorBlocksUUID(string refrigeratorUUID) => GetRefrigeratorBlocksUUID(this._context
        .RefrigeratorBlock.ToList(), refrigeratorUUID);

        public override void UpdateRefriData(RefriReeadDto refrigerator, string refrigeratorUUID) => UpdateRefriData(this._context.Refrigerator.ToList(), refrigerator, refrigeratorUUID);
        #endregion

        #region RegrigeratorBlocks
        public override RefrigeratorBlock GetRefriBlock(string blockUUID) => GetRefriItem(this._context
        .RefrigeratorBlock.ToList(), blockUUID);

        public override IList<string> GetRefriBlockSensorsUUID(string blockUUID) => this._context
        .SensorData.Where(
            sensor => sensor.RefrigeratorBlock
                          .BlockUUID.Equals(blockUUID)
        )
        .Select(sensor => sensor.SensorUUID)
        .ToList();

        public override void UpdBlocksRefriData(IList<string> blocksUUID, Refrigerator refrigerator) =>
            this._context
                .RefrigeratorBlock
                .Where(block => blocksUUID.Contains(block.BlockUUID))
                .ForEachAsync(block => block.Refrigerator = refrigerator);

        public override void CreateRefriBlock(RefrigeratorBlock refriBlock, out string blockUUID)
        {
            if (!_context.RefrigeratorBlock.Contains(refriBlock))
            {
                blockUUID = _encrypt.GetSHA512(_context.RefrigeratorBlock.Count().ToString());
                refriBlock.BlockUUID = blockUUID;
                _context.RefrigeratorBlock.Add(refriBlock);
            }
            else
            {
                throw new Exception("Блок холодильника с указанными данными уже существует !");
            }
        }

        public override void UpdateRefriBlockData(RefrigeratorBlock refriBlock)
        {
            var blockModel = _context
            .RefrigeratorBlock
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
            this._context
                .SensorData
                .Where(sensor => sensorsIDS.Contains(sensor.SensorUUID))
                .ForEachAsync(sensor => sensor.RefrigeratorBlock = refriBlock);
        #endregion

        #region SensorData
        public override void CreateSensor(SensorData sensor, out string sensorUUID)
        {
            if (!_context.SensorData.Contains(sensor))
            {
                sensorUUID = _encrypt.GetSHA512((_context.SensorData.Count() + 1).ToString());
                sensor.SensorUUID = sensorUUID;
                _context.SensorData.Add(sensor);
            }
            else
            {
                throw new Exception("Холодильник с указанными данными уже существует !");
            }
        }

        public override IList<SensorData> GetAllSensorData() => this._context.SensorData.ToList();

        public override SensorData GetSensor(string sensorUUID) => GetRefriItem(this._context
        .SensorData.ToList(), sensorUUID);
        #endregion
    }
}
