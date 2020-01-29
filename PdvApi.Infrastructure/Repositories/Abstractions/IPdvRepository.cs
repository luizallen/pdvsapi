using System.Collections.Generic;
using PdvApi.Infrastructure.Dtos;

namespace PdvApi.Infrastructure.Repositories.Abstractions
{
    public interface IPdvRepository
    {
        void CreatePdv(PdvDto pdv);
        PdvDto GetPdv(string pdvId);
        IList<PdvDto> GetPdv(string lng, string lat);
    }
}