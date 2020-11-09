using System;
using System.Collections.Generic;
using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RefrigeratorServerSide.Data;
using RefrigeratorServerSide.Data.RefriRepo;
using RefrigeratorServerSide.Dtos;
using RefrigeratorServerSide.Models;
using RemoteProvider.Models;

namespace RefrigeratorServerSide.Controllers
{
    [Route("api/block")]
    [ApiController]
    public class RefriBlockController: ControllerBase
    {
        private readonly IRefriRepo _refriRepo;
        private readonly IMapper _mapper;

        public RefriBlockController(IRefriRepo refriRepo, IMapper mapper)
        {
            this._refriRepo = refriRepo;
            this._mapper = mapper;
        }

        #region HttpPost
        /// <summary>
        /// Добавляет в БД инф. о новом блоке холодильника.
        /// </summary>
        /// <param name="refriBLock">Данные блока холодильника.</param>
        /// <response code="200">Идентификатор блока.</response>
        /// <response code="403">Процесс добавления данных о блоке холодильника в БД завершились ошибкой.</response>
        // POST api/refrigerator
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<string> CreateRefriBlock(RefriBlockWriteDto refriBLock)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(CreateRefriBlock)}: {JObject.FromObject(refriBLock).ToString()}");
                var modelBlock = _mapper.Map<RefrigeratorBlock>(refriBLock);
                _refriRepo.CreateRefriBlock(modelBlock, out string blockUUID);
                _refriRepo.SaveChanges();
                return Ok(JObject.FromObject(blockUUID));
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion
    }
}