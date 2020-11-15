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
        protected readonly IMapper _mapper;
        protected readonly Encrypt _encrypt = new();

        public BaseRefriRepo(IMapper mapper) => (_mapper) = (mapper);

        public abstract void CreateRefriBlock(RefrigeratorBlock refriBlock, out string blockUUID);
        public abstract void CreateRefrigerator(Refrigerator refrigerator);
        public abstract void CreateSensor(SensorData sensor, out string sensorUUID);
        public abstract IList<RefriReeadDto> GetAllRefri();
        public abstract RefrigeratorBlock GetRefriBlock(string blockUUID);
        public abstract IList<string> GetRefriBlockSensorsUUID(string blockUUID);
        public abstract Refrigerator GetRefrigerator(string refrigeratorUUID);
        public abstract IList<string> GetRefrigeratorBlocksUUID(string refrigeratorUUID);
        public abstract SensorData GetSensor(string sensorUUID);
        public abstract bool SaveChanges();
        public abstract void UpdateRefriBlockData(RefrigeratorBlock refriBlock);
        public abstract void UpdateRefriData(RefriReeadDto refrigerator, string refrigeratorUUID);
        public abstract void UpdBlocksRefriData(IList<string> blocksUUID, Refrigerator refrigerator);
        public abstract void UpdSensorsData(IList<string> sensorsIDS, RefrigeratorBlock refriBlock);
    }
}
