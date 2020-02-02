using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PdvApi.Infrastructure.Dtos;
using PdvApi.Infrastructure.Repositories.Abstractions;
using PdvApi.Models;
using System;
using System.Collections.Generic;
using ILogger = Serilog.ILogger;

namespace PdvApi.Controllers
{
    [ApiController]
    public class PdvController : ControllerBase
    {
        public IPdvQueryRepository PdvQueryRepository { get; }

        public IPdvCommandRepository PdvCommandRepository { get; set; }

        public IMapper Mapper { get; }

        public ILogger Logger { get; }

        public PdvController(
            IPdvQueryRepository pdvQueryRepository,
            IPdvCommandRepository pdvCommandRepository,
            IMapper mapper,
            ILogger logger)
        {
            PdvQueryRepository = pdvQueryRepository ?? throw new ArgumentNullException(nameof(pdvQueryRepository));
            PdvCommandRepository = pdvCommandRepository ?? throw new ArgumentNullException(nameof(pdvCommandRepository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("/pdv")]
        public IActionResult Create(Pdv pdvRequest)
        {
            var pdv = PdvQueryRepository.GetPdv(pdvRequest.Document);

            if (pdv != null)
                return BadRequest("This Pdv already exists");

            pdvRequest.Id = Guid.NewGuid();
            var pdvDto = Mapper.Map<PdvDto>(pdvRequest);
            PdvCommandRepository.CreatePdv(pdvDto);

            Logger.Information("Creating a PDV {pdvRequest}", pdvRequest);
            return Created("", pdvRequest);
        }

        [HttpGet("/pdv/{pdvId}")]
        public IActionResult Get(Guid pdvId)
        {
            var pdvResult = PdvQueryRepository.GetPdv(pdvId);

            if (pdvResult == null)
                return NotFound();

            var pdv = Mapper.Map<Pdv>(pdvResult);

            return Ok(pdv);
        }

        [HttpGet("/pdvs")]
        public IActionResult GetPdvs()
        {
            var pdvResult = PdvQueryRepository.GetPdvs();

            var pdv = Mapper.Map<List<Pdv>>(pdvResult);

            return Ok(pdv);
        }

        [HttpGet("/pdvs/around")]
        public IActionResult GetByCoordinates(string lng, string lat)
        {
            var pdvResult = PdvQueryRepository.GetInAreaPvs(lng, lat);

            var pdv = Mapper.Map<List<Pdv>>(pdvResult);

            return Ok(pdv);
        }
    }
}
