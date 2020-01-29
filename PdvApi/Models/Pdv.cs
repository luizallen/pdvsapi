using System;

namespace PdvApi.Models
{
    public class Pdv
    {
        public Guid Id { get; set; }
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public CoverageArea CoverageArea { get; set; }
        public Address Address { get; set; }
    }
}