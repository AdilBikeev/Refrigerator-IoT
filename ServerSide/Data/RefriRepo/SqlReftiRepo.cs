using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorServerSide.Data.RefriRepo
{
    public class SqlReftiRepo : IRefriRepo
    {
        private readonly RefrigeratorContext _context;

        public SqlReftiRepo(RefrigeratorContext context)
        {
            this._context = context;
        }

        public void AddOrUpdateReftInfo(SensorData sensorData)
        {
            var sensors = this._context.SensorData;
            var blocks = this._context.RefrigeratorBlock;
            var reftis = this._context.Refrigerator;

            var refri = reftis.FirstOrDefault(refr => refr.RefrigeratorUUID == sensorData.RefrigeratorBlock
                                                                                         .Refrigerator
                                                                                         .RefrigeratorUUID);
            if (refri == null)
            {
                reftis.Add(sensorData.RefrigeratorBlock.Refrigerator);
            }

            var block = blocks.FirstOrDefault(item => item.BlockUUID == sensorData.RefrigeratorBlock
                                                                                  .BlockUUID);
            if (block == null)
            {
                blocks.Add(sensorData.RefrigeratorBlock);
            }
            
            var sensor = sensors.FirstOrDefault(sens => sens.SensorUUID == sensorData.SensorUUID);
            if (sensor != null)
            {
                sensor.Value = sensorData.Value;
            }
            else
            {
                sensors.Add(sensorData);
            }

            this._context.SaveChanges();
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
    }
}
