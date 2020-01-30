using System;
using Dapper.Contrib.Extensions;

namespace PdvApi.Infrastructure.Dtos
{
    [Table("pdv")]
    public class PdvDto
    {
        public Guid Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public string CoverageAreaType { get; set; }
        public float[][][] CoverageAreaCoordinates { get; set; }
        public string AddressType { get; set; }
        public float[] AddressCoordinates { get; set; }
    }
}
