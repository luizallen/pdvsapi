using System;

namespace PdvApi.Infrastructure.Dtos
{
    public class PdvDto
    {
        public Guid Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public string CoverageAreaType { get; set; }
        public string[] CoverageAreaCoordinates { get; set; }
        public string AddressType { get; set; }
        public string[] AddressCoordinates { get; set; }
    }
}
