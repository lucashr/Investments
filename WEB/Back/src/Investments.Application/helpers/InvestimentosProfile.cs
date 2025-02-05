using AutoMapper;
using Investments.Application.Contracts;
using Investments.Application.Dtos;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Domain.Models;

namespace Investments.Application.helpers
{
    public class InvestmentsProfile : Profile
    {
        public InvestmentsProfile()
        {
            CreateMap<DetailedFund, BestFundRank>().ReverseMap();
            CreateMap<DetailedStock, BestStockRank>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<ApplicationUser, UserLoginDto>().ReverseMap();
            CreateMap<ApplicationUser, UserUpdateDto>().ReverseMap();
        }
    }
}