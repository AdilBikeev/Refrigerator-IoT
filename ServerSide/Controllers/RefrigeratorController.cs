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
    [Route("api/refrigerator")]
    [ApiController]
    public class RefrigeratorController: ControllerBase
    {
        private readonly IPlaceRepo _placeRepo;
        private readonly IRefriRepo _refriRepo;
        private readonly IMapper _mapper;

        public RefrigeratorController(IPlaceRepo placeRepo, IRefriRepo refriRepo, IMapper mapper)
        {
            this._placeRepo = placeRepo;
            this._refriRepo = refriRepo;
            this._mapper = mapper;
        }
        
        #region HttpGet
        /// <summary>
        /// ���������� ��� ����� � �������������.
        /// </summary>
        /// <response code="200">������ ������� ����������.</response>
        /// <response code="500">����������� ������ � ��.</response>
        // GET api/refrigerator
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Dictionary<string, List<Place>>> GetAllPlaces()
        {
            var repo = this._placeRepo.GetPlaceRefrigerator();

            return Ok(repo);
        }

        /// <summary>
        /// ���������� ������ ������������.
        /// </summary>
        /// <param name="refrigeratorUUID">UUID ������������.</param>
        /// <response code="200">������ ������� ���������� �������.</response>
        /// <response code="403">������� ������ ������ �� UUID ������������ ���������� �������.</response>
        // GET api/refrigerator/{refrigeratorUUID}
        [HttpGet]
        [Route("{refrigeratorUUID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<RefriReeadDto> GetRefrigerator(string refrigeratorUUID)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(UpdateRefrigeratorData)}: refrigeratorUUID={refrigeratorUUID}");

                var refrigerator = _refriRepo.GetRefrigerator(refrigeratorUUID);
                var refriModel = _mapper.Map<RefriReeadDto>(refrigerator);
                refriModel.blockIDS = _refriRepo.GetRefrigeratorBlocksUUID(refrigeratorUUID);

                return Ok(refriModel);
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion

        #region HttpPost
        /// <summary>
        /// ��������� ������ �������� ������������.
        /// </summary>
        /// <param name="sensorData">������ �������.</param>
        /// <response code="200">������ ������� ��������� � ��.</response>
        /// <response code="403">������� ���������� ������ � ������������ � �� ����������� �����������.</response>
        // POST api/refrigerator
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<string> UpdateRefrigeratorData(SensorData sensorData)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(UpdateRefrigeratorData)}: {JObject.FromObject(sensorData).ToString()}");

                _refriRepo.AddOrUpdateReftInfo(sensorData);
                return Ok();
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion
    }
}