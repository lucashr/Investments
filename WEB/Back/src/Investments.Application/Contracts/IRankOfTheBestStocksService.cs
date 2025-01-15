using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IRankOfTheBestStocksService
    {
        Task<IEnumerable<BestStockRank>> GetRankOfTheBestStocksAsync(int totalStocksRank = 0);
        Task<bool> AddRankOfTheBestStocksAsync(IEnumerable<BestStockRank> rankOfTheBestStocks);
        Task<double> CalculateScore(DetailedStock stock);
        double CalculateDividendConsistency(IEnumerable<StockDividend> dividends);
        double CalculateDividendCAGR(IEnumerable<StockDividend> dividends);
        string NormalizeString(string input);
        double Normalize(double value, double min, double max);
        IEnumerable<DetailedStock> FilterStocks(IEnumerable<DetailedStock> stocks);
    }
}