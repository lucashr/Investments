using AutoMapper;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain.Models;

namespace Investments.Application.helpers
{
    public class InvestmentsProfile : Profile
    {
        public InvestmentsProfile()
        {
            CreateMap<Stocks, StocksDto>().ReverseMap()
            .ForMember(dst => dst.MetaData,
                    map => map.MapFrom(src => src.MetaData));
            CreateMap<DetailedFunds, RankOfTheBestFunds>().ReverseMap();
        }
    }
}