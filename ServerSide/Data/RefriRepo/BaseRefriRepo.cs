using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RefrigeratorServerSide.Dtos;
using RemoteProvider.Models;
using System;
using System.Collections;
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

        protected T GetRefriItem<T>(IList<T> items, string UUID) where T : class
        {
            if (typeof(T) == typeof(Refrigerator))
            {
                return (T)Convert.ChangeType((items as IList<Refrigerator>).FirstOrDefault(item => item.RefrigeratorUUID.Equals(UUID)), typeof(T));
            }
            else if (typeof(T) == typeof(RefrigeratorBlock))
            {
                return (T)Convert.ChangeType((items as IList<RefrigeratorBlock>).FirstOrDefault(item => item.BlockUUID.Equals(UUID)), typeof(T));
            }
            else if (typeof(T) == typeof(SensorData))
            {
                return (T)Convert.ChangeType((items as IList<SensorData>).FirstOrDefault(item => item.SensorUUID.Equals(UUID)), typeof(T));
            }

            return null;
        }

        protected IList<string> GetRefrigeratorBlocksUUID<T>(IList<T> items, string refrigeratorUUID) where T : RefrigeratorBlock => items.Where(
            block => block.Refrigerator
                          .RefrigeratorUUID.Equals(refrigeratorUUID)
        )
        .Select(block => block.BlockUUID)
        .ToList();

        protected void UpdateRefriData<T>(IList<T> refriList, RefriReeadDto refrigerator, string refrigeratorUUID) where T : Refrigerator {
            var refriModel = refriList
            .FirstOrDefault(item =>
                item.RefrigeratorUUID.Equals(refrigeratorUUID)
             );


            if (refriModel is not null)
            {
                refriModel.Name = refrigerator.Name;
                refriModel.Description = refrigerator.Description;
                this.SaveChanges();
                this.UpdBlocksRefriData(refrigerator.blockIDS, refriModel);
            }
            else
            {
                throw new Exception("Холодильник с указанными данными не существует !");
            }
        }

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
