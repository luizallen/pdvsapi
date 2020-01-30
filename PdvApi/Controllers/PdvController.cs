using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdvApi.Infrastructure.Dtos;
using PdvApi.Infrastructure.Repositories.Abstractions;
using PdvApi.Models;
using System;
using System.Collections.Generic;

namespace PdvApi.Controllers
{
    [ApiController]
    [Route("/api/pdv")]
    public class PdvController : ControllerBase
    {
        public IPdvRepository PdvRepository { get; }

        public IMapper Mapper { get; }

        private readonly ILogger<PdvController> _logger;

        public PdvController(IPdvRepository pdvRepository,
            IMapper mapper,
            ILogger<PdvController> logger)
        {
            PdvRepository = pdvRepository ?? throw new ArgumentNullException(nameof(pdvRepository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public IActionResult Create(Pdv pdvRequest)
        {
            var pdv = PdvRepository.GetPdv(pdvRequest.Id);

            if (pdv != null)
                return BadRequest("This Pdv already exists");

            var pdvDto = Mapper.Map<PdvDto>(pdvRequest);
            PdvRepository.CreatePdv(pdvDto);

            _logger.LogInformation("Creating a PDV", pdvRequest);
            return Created("", pdvRequest);
        }

        [HttpGet("{pdvId}")]
        public IActionResult Get(Guid pdvId)
        {
            var pdvResult = PdvRepository.GetPdv(pdvId);

            if (pdvResult == null)
                return NotFound();

            var pdv = Mapper.Map<Pdv>(pdvResult);

            return Ok(pdv);
        }

        [HttpGet]
        public IActionResult GetByCoordinates(string lng, string lat)
        {
            var pdvResult = PdvRepository.GetPdv(lng, lat);

            var pdv = Mapper.Map<List<Pdv>>(pdvResult);

            return Ok(pdv);
        }
    }
}
