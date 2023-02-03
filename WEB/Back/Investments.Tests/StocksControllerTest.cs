// using System;
// using System.Collections.Generic;
// using AutoMapper;
// using Investments.API.Controllers;
// using Investments.Application;
// using Investments.Application.Contracts;
// using Investments.Application.Dtos;
// using Investments.Domain.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.DependencyInjection;
// using System.Linq;
// using Xunit;

// namespace Investments.Tests
// {


//     public class StocksControllerTest
//     {
//         [Fact]
//         public async void Teste()
//         {
//             TimeSeriesDailyDto timeSeriesDaily = new TimeSeriesDailyDto();
//             var modelNameResolver = new TimeSeriesDailyCustomResolver(timeSeriesDaily);
//             var config = new AutoMapper.MapperConfiguration(cfg =>
//             {
                
//                 cfg.CreateMap<Stocks, StocksDto>()
// 	            .ForMember(dest => dest.MetaData, opt => opt.MapFrom(
//                     src => new Application.Dtos.MetaData
//                     {
//                         Information = src.MetaData.Information,
//                         LastRefreshed = src.MetaData.LastRefreshed,
//                         OutputSize = src.MetaData.OutputSize,
//                         Symbol = src.MetaData.Symbol,
//                         TimeZone = src.MetaData.TimeZone
//                     }
//                 ))
//                 .ForMember(dest => dest.TimeSeriesDaily, opt => opt.MapFrom(modelNameResolver));
//             });

//             IMapper mapper = config.CreateMapper();

//             var stocksMock = new StocksService(mapper);

//             var stocksCtlr = new StocksController(stocksMock);

//             var result = await stocksCtlr.GetStockByCode("");
//             // Assert.Equal(2, convertidoObj.TimeSeriesDaily.Count);
//         }
//     }

    
// }
