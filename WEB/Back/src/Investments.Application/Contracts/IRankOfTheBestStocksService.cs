using System.Collections.Generic;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Models;

namespace Investments.Application.Contracts
{
    public interface IRankOfTheBestStocksService
    {
        Task<IEnumerable<RankOfTheBestStocks>> GetRankOfTheBestStocksAsync(int totalStocksRank = 0);
        Task<bool> AddRankOfTheBestStocksAsync(IEnumerable<RankOfTheBestStocks> rankOfTheBestStocks);
        Task<double> CalculateScore(DetailedStocks stock);
        double CalculateDividendConsistency(IEnumerable<StocksDividends> dividends);
        double CalculateDividendCAGR(IEnumerable<StocksDividends> dividends);
        string NormalizeString(string input);
        double Normalize(double value, double min, double max);
        IEnumerable<DetailedStocks> FilterStocks(IEnumerable<DetailedStocks> stocks);
    }
}