using AutoMapper;
using PdvApi.Infrastructure.Dtos;
using PdvApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace PdvApi.AutoMapper.Profiles
{
    [ExcludeFromCodeCoverage]
    public class PdvProfile : Profile
    {
        public PdvProfile()
        {
            CreateMap<PdvDto, Pdv>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.TradingName, o => o.MapFrom(s => s.TradingName))
                .ForMember(d => d.OwnerName, o => o.MapFrom(s => s.OwnerName))
                .ForMember(d => d.Document, o => o.MapFrom(s => s.Document))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.CoverageArea, o => o.MapFrom(s => s.CoverageArea));
        }
    }
}
