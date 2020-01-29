using PdvApi.Infrastructure.Dtos;
using System;
using System.Collections.Generic;

namespace PdvApi.Infrastructure.Repositories.Abstractions
{
    public interface IPdvRepository
    {
        void CreatePdv(PdvDto pdv);
        PdvDto GetPdv(Guid pdvId);
        IList<PdvDto> GetPdv(string lng, string lat);
    }
}