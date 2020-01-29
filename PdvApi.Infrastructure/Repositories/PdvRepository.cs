using System.Collections.Generic;
using PdvApi.Infrastructure.Dtos;
using PdvApi.Infrastructure.Repositories.Abstractions;

namespace PdvApi.Infrastructure.Repositories
{
    public class PdvRepository : IPdvRepository
    {
        public void CreatePdv(PdvDto pdv)
        {

        }

        public PdvDto GetPdv(string pdvId)
        {
            return new PdvDto();
        }

        public IList<PdvDto> GetPdv(string lng, string lat)
        {
           return new List<PdvDto>();
        }
    }
}
