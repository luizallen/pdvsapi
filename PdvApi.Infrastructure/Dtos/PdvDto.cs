using System;
using BAMCIS.GeoJSON;

namespace PdvApi.Infrastructure.Dtos
{

    public class PdvDto
    {
        public string Id { get; private set; }
        public string TradingName { get; private set; }
        public string OwnerName { get; private set; }
        public string Document { get; private set; }
        public MultiPolygon CoverageArea { get; private set; }
        public Point Address { get; private set; }

        public PdvDto(string id, 
            string tradingName, 
            string ownerName, 
            string document,
            MultiPolygon coverageArea,
            Point address)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            TradingName = tradingName ?? throw new ArgumentNullException(nameof(tradingName));
            OwnerName = ownerName ?? throw new ArgumentNullException(nameof(ownerName));
            Document = document ?? throw new ArgumentNullException(nameof(document));
            CoverageArea = coverageArea ?? throw new ArgumentNullException(nameof(coverageArea));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }
}
