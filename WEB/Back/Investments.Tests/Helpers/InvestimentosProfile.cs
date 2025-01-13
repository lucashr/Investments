using AutoMapper;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Tests.Helpers
{
    public class InvestmentsProfile : Profile
    {
        public InvestmentsProfile()
        {
            CreateMap<DetailedFunds, RankOfTheBestFunds>().ReverseMap();
            CreateMap<DetailedStocks, RankOfTheBestStocks>().ReverseMap();
        }
    }
}