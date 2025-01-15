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
            CreateMap<DetailedFund, BestFundRank>().ReverseMap();
            CreateMap<DetailedStock, BestStockRank>().ReverseMap();
        }
    }
}