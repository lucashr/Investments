using AutoMapper;
using Investments.Test.Application.Dtos;
using Investments.Test.Domain.Models;

namespace Investments.Test.Application.helpers
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