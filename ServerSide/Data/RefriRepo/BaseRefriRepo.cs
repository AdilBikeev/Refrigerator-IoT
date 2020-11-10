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
    /// Базовый класс для описания БД холодильника.
    /// </summary>
    public abstract class BaseRefriRepo : IRefriRepo
    {
        protected readonly RefrigeratorContext _context;
        protected readonly IMapper _mapper;
        protected readonly Encrypt _encrypt = new();

        public bool SaveChanges() => this._context.SaveChanges() >= 0;

        public BaseRefriRepo(RefrigeratorContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        #region Refrigerator
        public void CreateRefrigerator(Refrigerator refrigerator)
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

        public Refrigerator GetRefrigerator(string refrigeratorUUID) => this._context
        .Refrigerator.FirstOrDefault(
            item => item.RefrigeratorUUID.Equals(refrigeratorUUID)
        );

        public IList<string> GetRefrigeratorBlocksUUID(string refrigeratorUUID) => this._context
        .RefrigeratorBlock.Where(
            block => block.Refrigerator
                          .RefrigeratorUUID.Equals(refrigeratorUUID)
        )
        .Select(block => block.BlockUUID)
        .ToList();

        public void UpdateRefriData(RefriReeadDto refrigerator, string refrigeratorUUID)
        {
            var refriModel = _context
            .Refrigerator
            .FirstOrDefault(item =>
                item.RefrigeratorUUID.Equals(refrigeratorUUID)
             );


            if (refriModel is not null)
            {
                refriModel = _mapper.Map<Refrigerator>(refrigerator);
                this.UpdBlocksRefriData(refrigerator.blockIDS, refriModel);
            }
            else
            {
                throw new Exception("Холодильник с указанными данными не существует !");
            }
        }
        #endregion

        #region RegrigeratorBlocks
        public RefrigeratorBlock GetRefriBlock(string blockUUID) => this._context
        .RefrigeratorBlock.FirstOrDefault(
            item => item.BlockUUID.Equals(blockUUID)
        );

        public IList<string> GetRefriBlockSensorsUUID(string blockUUID) => this._context
        .SensorData.Where(
            sensor => sensor.RefrigeratorBlock
                          .BlockUUID.Equals(blockUUID)
        )
        .Select(sensor => sensor.SensorUUID)
        .ToList();

        public void UpdBlocksRefriData(IList<string> blocksUUID, Refrigerator refrigerator) =>
            this._context
                .RefrigeratorBlock
                .Where(block => blocksUUID.Contains(block.BlockUUID))
                .ForEachAsync(block => block.Refrigerator = refrigerator);

        public void CreateRefriBlock(RefrigeratorBlock refriBlock, out string blockUUID)
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

        public void UpdateRefriBlockData(RefrigeratorBlock refriBlock)
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

        public void UpdSensorsData(IList<string> sensorsIDS, RefrigeratorBlock refriBlock) =>
            this._context
                .SensorData
                .Where(sensor => sensorsIDS.Contains(sensor.SensorUUID))
                .ForEachAsync(sensor => sensor.RefrigeratorBlock = refriBlock);
        #endregion

        #region SensorData
        public void CreateSensor(SensorData sensor, out string sensorUUID)
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

        public SensorData GetSensor(string sensorUUID) => this._context
        .SensorData.FirstOrDefault(
            item => item.SensorUUID.Equals(sensorUUID)
        );
        #endregion
    }
}
