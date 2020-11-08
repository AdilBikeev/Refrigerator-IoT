using Microsoft.EntityFrameworkCore;
using RemoteProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace RefrigeratorServerSide.Data.RefriRepo
{
    public class SqlReftiRepo : IRefriRepo
    {
        private readonly RefrigeratorContext _context;
        private readonly Encrypt _encrypt = new();

        public SqlReftiRepo(RefrigeratorContext context)
        {
            this._context = context;
        }

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
        #endregion

        #region RegrigeratorBlocks
        public void UpdBlocksRefriData(IList<string> blocksUUID, Refrigerator refrigerator) =>
            this._context
                .RefrigeratorBlock
                .Where(block => blocksUUID.Contains(block.BlockUUID))
                .ForEachAsync(block => block.Refrigerator = refrigerator);
        #endregion
    }
}
