using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PdvApi.Requests
{
    public class PdvRequest
    {
        public string TradingName { get; set; }
        public string OwnerName { get; set; }
        public string Document { get; set; }
        public CoverageArea CoverageArea { get; set; }
        public Address Address { get; set; }
    }
}
