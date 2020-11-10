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

        #region HttpGet
        /// <summary>
        /// ���������� ������ ������������.
        /// </summary>
        /// <param name="sensorUUID">UUID �������.</param>
        /// <response code="200">������ ������� ���������� �������.</response>
        /// <response code="403">������� ������ ������ �� UUID ������� ���������� �������.</response>
        // GET api/sensor/{sensorUUID}
        [HttpGet]
        [Route("{sensorUUID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<RefriReeadDto> GetSensor(string sensorUUID)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(GetSensor)}: refrigeratorUUID={sensorUUID}");

                var sensor = _refriRepo.GetSensor(sensorUUID);
                return Ok(_mapper.Map<SensorReeadDto>(sensor));
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion

        #region HttpPost
        /// <summary>
        /// ��������� � �� ���. � ����� �������.
        /// </summary>
        /// <param name="sensor">������ �������.</param>
        /// <response code="200">������ ���������� ������������� �������.</response>
        /// <response code="403">������� ���������� ������ � ������� � �� ����������� �������.</response>
        // POST api/sensor
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<string> CreateSensor(SensorReeadDto sensor)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(CreateSensor)}: {JObject.FromObject(sensor).ToString()}");
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