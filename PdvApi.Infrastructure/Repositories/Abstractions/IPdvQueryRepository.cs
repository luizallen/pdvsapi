using PdvApi.Infrastructure.Dtos;
using System;
using System.Collections.Generic;

namespace PdvApi.Infrastructure.Repositories.Abstractions
{
    public interface IPdvQueryRepository
    {
        PdvDto GetPdv(Guid pdvId);
        PdvDto GetPdv(string document);
        IList<PdvDto> GetPdvs();
        IList<PdvDto> GetInAreaPvs(string lng, string lat);
    }
}