using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RefrigeratorServerSide.Data;
using RefrigeratorServerSide.Data.RefriRepo;
using RefrigeratorServerSide.Dtos;
using RemoteProvider.Models;

namespace RefrigeratorServerSide.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class RefrigeratorController: ControllerBase
    {
        private readonly IRefriRepo _refriRepo;
        private readonly IMapper _mapper;

        public RefrigeratorController(IRefriRepo refriRepo, IMapper mapper)
        {
            this._refriRepo = refriRepo;
            this._mapper = mapper;
        }

        #region Refrigerator

        #region HttpGet
        /// <summary>
        /// Возвращает данные всех холодильников.
        /// </summary>
        /// <response code="200">Данные успешно возвращены.</response>
        /// <response code="500">Отсутствуют данные в БД.</response>
        // GET api/refrigerator
        [HttpGet]
        [Route("refrigerator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<Refrigerator>> GetAllRefri() => Ok(this._refriRepo.GetAllRefri());

        /// <summary>
        /// Возвращает данные холодильника.
        /// </summary>
        /// <param name="refrigeratorUUID">UUID холодильника.</param>
        /// <response code="200">Данные успешно отправлены клиенту.</response>
        /// <response code="403">Процесс поиска данных по UUID холодильника завершился ошибкой.</response>
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
                    return Forbid("Не удалсоь найти холодильник с заданным идентификатроом");
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
        /// Добавляет в БД инф. о новом холодильнике.
        /// </summary>
        /// <param name="refrigerator">Данные холодильника.</param>
        /// <response code="200">Инф. о холодильнике успешно добавлена в БД.</response>
        /// <response code="403">Процесс добавления данных о холодильнике в БД завершились ошибкой.</response>
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
        /// Обновляет данные холодильника.
        /// </summary>
        /// <param name="refriDto">Обновленные данные холодильника.</param>
        /// <param name="refrigeratorUUID">UUID холодильника.</param>
        /// <response code="200">Данные успешно обновлены.</response>
        /// <response code="403">Процесс обновления данных холодильника завершился ошибкой.</response>
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
        /// Добавляет в БД инф. о новом блоке холодильника.
        /// </summary>
        /// <param name="refriBLock">Данные блока холодильника.</param>
        /// <response code="200">Идентификатор блока.</response>
        /// <response code="403">Процесс добавления данных о блоке холодильника в БД завершились ошибкой.</response>
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
        /// Обновляет данные блока холодильника.
        /// </summary>
        /// <param name="refriBLock">Обновленные данные блока холодильника.</param>
        /// <param name="blockUUID">UUID блока холодильника.</param>
        /// <response code="200">Данные успешно обновлены.</response>
        /// <response code="403">Процесс обновления данных завершился ошибкой.</response>
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
        /// Возвращает данные всех блоков холодильника.
        /// </summary>
        /// <response code="200">Данные успешно возвращены.</response>
        /// <response code="500">Отсутствуют данные в БД.</response>
        // GET api/block
        [HttpGet]
        [Route("block")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<RefrigeratorBlock>> GetAllRefriBlock() => Ok(this._refriRepo.GetAllRefriBlock());

        /// <summary>
        /// Возвращает данные блока холодильника.
        /// </summary>
        /// <param name="blockUUID">UUID блока холодильника.</param>
        /// <response code="200">Данные успешно отправлены клиенту.</response>
        /// <response code="403">Процесс поиска данных по UUID блока холодильника завершился ошибкой.</response>
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
        /// Возвращает данные всех сенсоров блока холодильника.
        /// </summary>
        /// <response code="200">Данные успешно возвращены.</response>
        /// <response code="500">Отсутствуют данные в БД.</response>
        // GET api/sensor
        [HttpGet]
        [Route("sensor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<SensorData>> GetAllSensorData() => Ok(this._refriRepo.GetAllSensorData());

        /// <summary>
        /// Возвращает данные холодильника.
        /// </summary>
        /// <param name="sensorUUID">UUID сенсора.</param>
        /// <response code="200">Данные успешно отправлены клиенту.</response>
        /// <response code="403">Процесс поиска данных по UUID сенсора завершился ошибкой.</response>
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
        /// Добавляет в БД инф. о новом сенсоре.
        /// </summary>
        /// <param name="sensor">Данные сенсора.</param>
        /// <response code="200">Сервис возвращает идентификатор сенсора.</response>
        /// <response code="403">Процесс добавления данных о сенсоре в БД завершились ошибкой.</response>
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
