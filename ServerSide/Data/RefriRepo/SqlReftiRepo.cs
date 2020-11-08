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
    public class SqlReftiRepo : IRefriRepo
    {
        private readonly RefrigeratorContext _context;
        private readonly IMapper _mapper;
        private readonly Encrypt _encrypt = new();

        public SqlReftiRepo(RefrigeratorContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public bool SaveChanges() => this._context.SaveChanges() >= 0;

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
        public void UpdBlocksRefriData(IList<string> blocksUUID, Refrigerator refrigerator) =>
            this._context
                .RefrigeratorBlock
                .Where(block => blocksUUID.Contains(block.BlockUUID))
                .ForEachAsync(block => block.Refrigerator = refrigerator);
        #endregion
    }
}
