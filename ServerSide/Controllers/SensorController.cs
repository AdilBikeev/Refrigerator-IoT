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
    [Route("api/sensor")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly IRefriRepo _refriRepo;
        private readonly IMapper _mapper;

        public SensorController(IRefriRepo refriRepo, IMapper mapper)
        {
            this._refriRepo = refriRepo;
            this._mapper = mapper;
        }

        #region HttpPost
        /// <summary>
        /// Добавляет в БД инф. о новом сенсоре.
        /// </summary>
        /// <param name="sensor">Данные сенсора.</param>
        /// <response code="200">Сервис возвращает идентификатор сенсора.</response>
        /// <response code="403">Процесс добавления данных о сенсоре в БД завершились ошибкой.</response>
        // POST api/sensor
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<string> CreateRefriBlock(SensorReeadDto sensor)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(CreateRefriBlock)}: {JObject.FromObject(sensor).ToString()}");
                var sensorModel = _mapper.Map<SensorData>(sensor);
                _refriRepo.CreateSensor(sensorModel, out string sensorUUID);
                _refriRepo.SaveChanges();
                return Ok(sensorUUID);
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion
    }
}