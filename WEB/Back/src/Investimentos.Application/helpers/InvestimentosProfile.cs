using AutoMapper;
using Investimentos.Application.Contracts;
using Investimentos.Application.Dtos;
using Investimentos.Domain.Models;

namespace Investimentos.Application.helpers
{
    public class InvestimentosProfile : Profile
    {
        public InvestimentosProfile()
        {
            CreateMap<Stocks, StocksDto>().ReverseMap()
            .ForMember(dst => dst.MetaData,
                    map => map.MapFrom(src => src.MetaData));
            CreateMap<DetailedFunds, RankOfTheBestFunds>().ReverseMap();
        }
    }
}