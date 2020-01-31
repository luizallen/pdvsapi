using PdvApi.Infrastructure.Dtos;
using System;
using System.Collections.Generic;

namespace PdvApi.Infrastructure.Repositories.Abstractions
{
    public interface IPdvRepository
    {
        void CreatePdv(PdvDto pdv);
        PdvDto GetPdv(Guid pdvId);
        PdvDto GetPdv(string pdvId);
        IList<PdvDto> GetPdvs();
        IList<PdvDto> GetInAreaPvs(string lng, string lat);
    }
}