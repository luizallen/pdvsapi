using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdvApi.Infrastructure.Repositories.Abstractions;
using PdvApi.Requests;

namespace PdvApi.Controllers
{
    [ApiController]
    [Route("pdv")]
    public class PdvController : ControllerBase
    {
        public IPdvRepository PdvRepository;

        private readonly ILogger<PdvController> _logger;

        public PdvController(IPdvRepository pdvRepository,
            ILogger<PdvController> logger)
        {
            PdvRepository = pdvRepository ?? throw new ArgumentNullException(nameof(pdvRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public IActionResult Get(PdvRequest pdvRequest)
        {
            _logger.LogInformation("Creating a PDV", pdvRequest);
            return Created("", pdvRequest);
        }

        [HttpGet("{pdvId}")]
        public IActionResult Get(string pdvId)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(string lng, string lat)
        {
            return Ok();
        }
    }
}
