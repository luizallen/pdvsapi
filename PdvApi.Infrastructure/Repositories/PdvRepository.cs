using PdvApi.Infrastructure.Dtos;
using PdvApi.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;

namespace PdvApi.Infrastructure.Repositories
{
    public class PdvRepository : IPdvRepository
    {
        public void CreatePdv(PdvDto pdv)
        {

        }

        public PdvDto GetPdv(Guid pdvId)
        {
            return new PdvDto();
        }

        public IList<PdvDto> GetPdv(string lng, string lat)
        {
           return new List<PdvDto>();
        }
    }
}
