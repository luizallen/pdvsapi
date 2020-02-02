using PdvApi.Infrastructure.Dtos;

namespace PdvApi.Infrastructure.Repositories.Abstractions
{
    public interface IPdvCommandRepository
    {
        void CreatePdv(PdvDto pdv);
        string CommandPdvInsert { get; }
    }
}