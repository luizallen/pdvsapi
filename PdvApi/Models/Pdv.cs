using BAMCIS.GeoJSON;
using System;

namespace PdvApi.Models
{
    public class Pdv
    {
        public Guid Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public MultiPolygon CoverageArea { get; set; }
        public Point Address { get; set; }
    }
}