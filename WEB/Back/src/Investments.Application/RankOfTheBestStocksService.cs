using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Domain.Models;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class RankOfTheBestStocksService : IRankOfTheBestStocksService
    {
        private readonly IRankOfTheBestStocksPersist _rankOfTheBestStocksPersist;
        private readonly IDetailedStockService _detailedStocksService;
        private readonly IStocksDividendService _stocksDividendsService;
        private readonly IMapper _mapper;

        public RankOfTheBestStocksService(IRankOfTheBestStocksPersist rankOfTheBestStocksPersist,
                                          IDetailedStockService detailedStocksService,
                                          IStocksDividendService stocksDividendsService,
                                          IMapper mapper)
        {
            _rankOfTheBestStocksPersist = rankOfTheBestStocksPersist;
            _detailedStocksService = detailedStocksService;
            _stocksDividendsService = stocksDividendsService;
            _mapper = mapper;
        }

        /*
            Normalização: Os valores dos indicadores são normalizados entre 0 e 1 com base em limites mínimos e máximos definidos para cada métrica.
            Pesos: Cada métrica tem um peso atribuído para refletir sua importância relativa na decisão.
            Pontuação Final: A soma ponderada das métricas fornece o score final para cada ação.
            Ordenação: As ações são classificadas em ordem decrescente de score.

            Indicadores de Valuation (Preço/Valor)

            P/L Ajustado: Ajustar o P/L para descontar eventos não recorrentes, como vendas de ativos ou ganhos extraordinários.
            PEG Ratio: Razão entre P/L e crescimento do lucro esperado. Mede se o preço da ação é justificado pelo crescimento.
            EV/EBITDA Ajustado: Avaliar o valuation desconsiderando efeitos de endividamento e despesas não operacionais.

            Indicadores de Solidez Financeira

            Dívida Líquida / EBITDA: Mede a capacidade da empresa de pagar suas dívidas com o lucro operacional.
            Índice de Cobertura de Juros (EBIT / Despesas Financeiras): Indica a capacidade de honrar despesas com juros.
            Alavancagem Patrimonial (Dívida Total / Patrimônio Líquido): Mostra o nível de endividamento em relação ao patrimônio.

            Indicadores de Rentabilidade

            Margem Líquida: Percentual do lucro líquido sobre a receita total.
            ROA (Retorno sobre Ativos): Indica o lucro gerado por cada unidade de ativo da empresa.
            Crescimento do Lucro por Ação (EPS Growth): Mede a evolução dos lucros por ação em um período específico.

            Indicadores de Mercado e Liquidez

            Beta da Ação: Mede a volatilidade em relação ao mercado. Ajuda a identificar riscos.
            Volume Médio de Negociação (3 meses): Garante que a ação tenha liquidez suficiente.
            Book-to-Market Ratio: Inverso do P/VPA, usado para encontrar ações subavaliadas.

            Indicadores de Crescimento

            CAGR de Receita (Últimos 5 Anos): Taxa composta de crescimento anual da receita.
            CAGR de EBITDA (Últimos 5 Anos): Mede o crescimento do lucro operacional.
            Crescimento dos Dividendos (Últimos 5 Anos): Mostra a consistência e crescimento no pagamento de dividendos.

            Indicadores ESG (Ambiental, Social e Governança)

            Pontuação ESG: Indicador qualitativo/quantitativo para avaliar práticas ambientais, sociais e de governança.
            Percentual de Emissões Reduzidas: Pode ser usado para empresas sensíveis a questões climáticas.
            Presença no Índice de Sustentabilidade (ISE): Identifica empresas com práticas sustentáveis.

            Indicadores de Eficiência Operacional

            Ciclo Financeiro (Dias): Tempo médio para converter recursos em caixa, considerando o ciclo operacional e financeiro.
            Margem EBITDA: Indica o lucro operacional em relação à receita total, medindo eficiência operacional.
            Capex / Receita: Percentual do investimento em bens de capital em relação à receita, importante em setores intensivos em capital.
        */

        public async Task<IEnumerable<BestStockRank>> GetRankOfTheBestStocksAsync(int totalStocksRank = 0)
        {
            var stocks = await _detailedStocksService.GetAllDetailedStocksAsync();
            stocks = FilterStocks(stocks);
            
            var stockScores = await Task.WhenAll(stocks.Select(async stock =>
            {
                double score = await CalculateScore(stock);
                return new { Stock = stock, Score = score };
            }));

            foreach (var stock in stockScores.OrderByDescending(stock => stock.Score))
            {
                Debug.WriteLine($"{stock.Stock.FundCode} - {stock.Score}");
            }

            var rankedStocks = stockScores.OrderByDescending(stock => stock.Score)
                                          .Select(stock => stock.Stock);

            var result = _mapper.Map<IEnumerable<BestStockRank>>(rankedStocks);  
                    
            return  result;

        }

        public Task<bool> AddRankOfTheBestStocksAsync(IEnumerable<BestStockRank> rankOfTheBestStocks)
        {
            throw new NotImplementedException();
        }

        public async Task<double> CalculateScore(DetailedStock stock)
        {

            double score = 0;

            // Critérios de pontuação
            score += Normalize(stock.DivYield, 5, 10) * 0.3;         // Dividend Yield (30%)
            score += Normalize(stock.LiquidityTwoMonths, 10000, 100000) * 0.2; // Liquidez (20%)
            score += Normalize(1 / stock.PL, 0.05, 0.15) * 0.1;      // Preço sobre Lucro (10%)
            score += Normalize(stock.ROE, 10, 25) * 0.15;           // Retorno sobre Patrimônio (15%)
            score += Normalize(stock.ROIC, 10, 20) * 0.1;           // Retorno sobre Capital Investido (10%)
            score += Normalize(stock.RevenueGrowthFiveYears, 5, 15) * 0.1; // Crescimento Receita (10%)
            score += Normalize(stock.EbitMargin, 10, 30) * 0.05;    // Margem EBIT (5%)
            score += Normalize(stock.NetWorth / stock.GrossEquityDebt, 1, 3) * 0.1;  // Solidez Financeira (10%)
            score += Normalize(stock.EVEBITDA, 5, 12) * 0.05;                        // EV/EBITDA (5%)
            score += Normalize(stock.LiquidityMargin, 1, 3) * 0.05;                  // Margem de Liquidez (5%)
            score += Normalize(stock.GrossEquityDebt / stock.NetWorth, 0.5, 2) * 0.1; // Alavancagem patrimonial (10%)

            var dividends = await _stocksDividendsService.GetStockDividendsByCodeAsync(stock.FundCode);
            
            if(dividends.Count() > 0)
            {
                
                dividends = dividends.Where(x => NormalizeString(x.Type) == "DIVIDENDO" || NormalizeString(x.Type) == "JRS CAP PROPRIO");

                if(dividends.Count() > 0)
                {
                    // Métricas de dividendos
                    double consistency = CalculateDividendConsistency(dividends);
                    double cagr = CalculateDividendCAGR(dividends);
                    
                    score += Normalize(consistency, 0.5, 1) * 0.2;            // Consistência (20%)
                    score += Normalize(cagr, 0.05, 0.2) * 0.15;               // Crescimento (15%)

                }   
                
            }

            return score;
        }

        public string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Normaliza para decompor caracteres com acento
            var normalizedText = input.Normalize(NormalizationForm.FormD);

            // Remove acentos e retorna ao formato normalizado
            return new string(normalizedText
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray())
                .Normalize(NormalizationForm.FormC);
        }

        // Calcular a consistência dos dividendos ao longo dos anos
        public double CalculateDividendConsistency(IEnumerable<StockDividend> dividends)
        {

            int totalYears = dividends
            .Select(d => DateTime.Parse(d.Date).Year)
            .Distinct()
            .Count();

            int yearsWithDividends = dividends
                .GroupBy(d => DateTime.Parse(d.Date).Year)
                .Count(g => g.Sum(d => d.Value) > 0);

            return (double)yearsWithDividends / totalYears;

        }

        // Calcular o crescimento anual composto (CAGR) dos dividendos
        public double CalculateDividendCAGR(IEnumerable<StockDividend> dividends)
        {

            var groupedDividends = dividends
                                    .GroupBy(d => DateTime.Parse(d.Date).Year)
                                    .Select(g => new { Year = g.Key, TotalDividend = g.Sum(d => d.Value) })
                                    .OrderBy(d => d.Year)
                                    .ToList();

            if (groupedDividends.Count < 2) return 0;

            double firstYear = groupedDividends.First().TotalDividend;
            double lastYear = groupedDividends.Last().TotalDividend;
            int years = groupedDividends.Last().Year - groupedDividends.First().Year;

            return Math.Pow(lastYear / firstYear, 1.0 / years) - 1;

        }

        public double Normalize(double value, double min, double max)
        {

            double score = 0;

            if (value <= min) return 0;
            if (value >= max) return 1;

            score = (value - min) / (max - min);

            if(double.IsNaN(score))
                score = 0;

            return score;
                
        }

        public IEnumerable<DetailedStock> FilterStocks(IEnumerable<DetailedStock> stocks)
        {
            return stocks.Where(stock => 
                stock.DivYield >= 3 &&  // Dividend Yield mínimo
                stock.LiquidityTwoMonths >= 1000000 &&  // Liquidez mínima
                stock.ROE > 0 &&  // ROE positivo
                stock.PVP <= 3 && // P/VP aceitável
                stock.Quotation > 0  // Preço positivo
            ).ToList();
        }

    }
}