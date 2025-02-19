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
            // CreateMap<DetailedFund, BestFundRank>().ReverseMap();
            // CreateMap<DetailedStock, BestStockRank>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
                // .ForMember(dest => dest.Password, opt => opt.Ignore())
                // .ForMember(dest => dest.Token, opt => opt.Ignore())
                // .ForMember(dest => dest.ZipCode, opt => opt.Ignore())
                // .ForMember(dest => dest.Address, opt => opt.Ignore())
                // .ForMember(dest => dest.District, opt => opt.Ignore())
                // .ForMember(dest => dest.City, opt => opt.Ignore())
                // .ForMember(dest => dest.State, opt => opt.Ignore());

            // CreateMap<UserDto, User>()
            //     .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            //     .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            //     .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            //     .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            //     .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            //     .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            //     .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
            //     .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            //     .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
            //     .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            //     .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            //     .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());
                
            CreateMap<ApplicationUser, UserDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
                // .ForMember(dest => dest.Password, opt => opt.Ignore())
                // .ForMember(dest => dest.ZipCode, opt => opt.Ignore())
                // .ForMember(dest => dest.Address, opt => opt.Ignore())
                // .ForMember(dest => dest.District, opt => opt.Ignore())
                // .ForMember(dest => dest.City, opt => opt.Ignore())
                // .ForMember(dest => dest.State, opt => opt.Ignore())
                // .ForMember(dest => dest.Token, opt => opt.Ignore());

            // CreateMap<UserDto, ApplicationUser>()
            //     .ForMember(dest => dest.Function, opt => opt.Ignore())
            //     .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
            //     .ForMember(dest => dest.EnderecoUsuario, opt => opt.Ignore());

            CreateMap<ApplicationUser, UserLoginDto>().ReverseMap().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));;

            CreateMap<UserLoginDto, ApplicationUser>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
                // .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                // .ForMember(dest => dest.LastName, opt => opt.Ignore())
                // .ForMember(dest => dest.Function, opt => opt.Ignore())
                // .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                // .ForMember(dest => dest.EnderecoUsuario, opt => opt.Ignore())
                // .ForMember(dest => dest.Version, opt => opt.Ignore())
                // .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                // .ForMember(dest => dest.Claims, opt => opt.Ignore())
                // .ForMember(dest => dest.Roles, opt => opt.Ignore())
                // .ForMember(dest => dest.Logins, opt => opt.Ignore())
                // .ForMember(dest => dest.Tokens, opt => opt.Ignore())
                // .ForMember(dest => dest.Id, opt => opt.Ignore())
                // .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                // .ForMember(dest => dest.Email, opt => opt.Ignore())
                // .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                // .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                // .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                // .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                // .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                // .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                // .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                // .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                // .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                // .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                // .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());

            CreateMap<ApplicationUser, UserUpdateDto>().ReverseMap().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));;
            
        }
    }
}