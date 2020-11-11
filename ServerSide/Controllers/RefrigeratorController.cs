using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    [Route("api")]
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

        #region Refrigerator

        #region HttpGet
        /// <summary>
        /// ���������� ��� ����� � �������������.
        /// </summary>
        /// <response code="200">������ ������� ����������.</response>
        /// <response code="500">����������� ������ � ��.</response>
        // GET api/refrigerator
        [HttpGet]
        [Route("refrigerator")]
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
        [Route("refrigerator/{refrigeratorUUID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<RefriReeadDto> GetRefrigerator(string refrigeratorUUID)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(GetRefrigerator)}: refrigeratorUUID={refrigeratorUUID}");

                var refrigerator = _refriRepo.GetRefrigerator(refrigeratorUUID);
                if (refrigerator is null)
                    return Forbid("�� ������� ����� ����������� � �������� ���������������");
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
        #region Refrigerator
        /// <summary>
        /// ��������� � �� ���. � ����� ������������.
        /// </summary>
        /// <param name="refrigerator">������ ������������.</param>
        /// <response code="200">���. � ������������ ������� ��������� � ��.</response>
        /// <response code="403">������� ���������� ������ � ������������ � �� ����������� �������.</response>
        // POST api/refrigerator
        [HttpPost]
        [Route("refrigerator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult CreateRefrigerator(RefriReeadDto refrigerator)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(CreateRefrigerator)}: {JObject.FromObject(refrigerator).ToString()}");
                var modelRefri = _mapper.Map<Refrigerator>(refrigerator);
                _refriRepo.CreateRefrigerator(modelRefri);
                _refriRepo.SaveChanges();
                _refriRepo.UpdBlocksRefriData(refrigerator.blockIDS, modelRefri);
                _refriRepo.SaveChanges();
                return Ok();
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }

        /// <summary>
        /// ��������� ������ ������������.
        /// </summary>
        /// <param name="refriDto">����������� ������ ������������.</param>
        /// <param name="refrigeratorUUID">UUID ������������.</param>
        /// <response code="200">������ ������� ���������.</response>
        /// <response code="403">������� ���������� ������ ������������ ���������� �������.</response>
        // POST api/refrigerator/{refrigeratorUUID}
        [HttpPost]
        [Route("refrigerator/{refrigeratorUUID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult UpdateRefrigerator([FromBody] RefriReeadDto refriDto, [FromRoute] string refrigeratorUUID)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(UpdateRefrigerator)}: refriDto={JObject.FromObject(refriDto)}, refrigeratorUUID={refrigeratorUUID}");

                _refriRepo.UpdateRefriData(refriDto, refrigeratorUUID);
                _refriRepo.SaveChanges();
                return Ok();
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion
        #endregion

        #endregion

        #region RefriBlock
        #region HttpPost
        /// <summary>
        /// ��������� � �� ���. � ����� ����� ������������.
        /// </summary>
        /// <param name="refriBLock">������ ����� ������������.</param>
        /// <response code="200">������������� �����.</response>
        /// <response code="403">������� ���������� ������ � ����� ������������ � �� ����������� �������.</response>
        // POST api/block
        [HttpPost]
        [Route("block")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<string> CreateRefriBlock(RefriBlockReadDto refriBLock)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(CreateRefriBlock)}: {JObject.FromObject(refriBLock).ToString()}");
                var blockModel = _mapper.Map<RefrigeratorBlock>(refriBLock);
                _refriRepo.CreateRefriBlock(blockModel, out string blockUUID);
                _refriRepo.SaveChanges();
                _refriRepo.UpdSensorsData(refriBLock.SensorsIDS, blockModel);
                _refriRepo.SaveChanges();
                return Ok(JObject.FromObject(blockUUID));
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }

        /// <summary>
        /// ��������� ������ ����� ������������.
        /// </summary>
        /// <param name="refriBLock">����������� ������ ����� ������������.</param>
        /// <param name="blockUUID">UUID ����� ������������.</param>
        /// <response code="200">������ ������� ���������.</response>
        /// <response code="403">������� ���������� ������ ���������� �������.</response>
        // POST api/block/{blockUUID}
        [HttpPost]
        [Route("block/{blockUUID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult UpdateRefriBlock([FromBody] RefriBlockReadDto refriBLock, [FromRoute] string blockUUID)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(UpdateRefriBlock)}: refriDto={JObject.FromObject(refriBLock)}, blockUUID={blockUUID}");

                var blockModel = _mapper.Map<RefrigeratorBlock>(refriBLock);
                blockModel.BlockUUID = blockUUID;
                _refriRepo.UpdateRefriBlockData(blockModel);
                _refriRepo.SaveChanges();
                _refriRepo.UpdSensorsData(refriBLock.SensorsIDS, blockModel);
                _refriRepo.SaveChanges();
                return Ok();
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion

        #region HttpGet
        /// <summary>
        /// ���������� ������ ����� ������������.
        /// </summary>
        /// <param name="blockUUID">UUID ����� ������������.</param>
        /// <response code="200">������ ������� ���������� �������.</response>
        /// <response code="403">������� ������ ������ �� UUID ����� ������������ ���������� �������.</response>
        // GET api/block/{blockUUID}
        [HttpGet]
        [Route("block/{blockUUID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<RefriBlockReadDto> GetRefriBlock(string blockUUID)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(GetRefriBlock)}: blockUUID={blockUUID}");

                var refriblock = _refriRepo.GetRefriBlock(blockUUID);
                var blockModel = _mapper.Map<RefriBlockReadDto>(refriblock);
                blockModel.SensorsIDS = _refriRepo.GetRefrigeratorBlocksUUID(blockUUID);

                return Ok(blockModel);
            }
            catch (Exception exc)
            {
                return Forbid(exc.ToString());
            }
        }
        #endregion
        #endregion

        #region SensorData
        #region HttpGet
        /// <summary>
        /// ���������� ������ ������������.
        /// </summary>
        /// <param name="sensorUUID">UUID �������.</param>
        /// <response code="200">������ ������� ���������� �������.</response>
        /// <response code="403">������� ������ ������ �� UUID ������� ���������� �������.</response>
        // GET api/sensor/{sensorUUID}
        [HttpGet]
        [Route("sensor/{sensorUUID}")]
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
        [Route("sensor")]
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
        #endregion
    }
}