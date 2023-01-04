using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Application.Dtos
{
    public class FundsDto
    {

        public string Papel { get; set; }
        public string Segmento { get; set; }
        public double Cotacao { get; set; }
        public double FFOYield { get; set; }
        public double DividendYield { get; set; }
        public double PVP { get; set; }
        public double ValorDeMercado { get; set; }
        public double Liquidez { get; set; }
        public double QtdDeImoveis { get; set; }
        public double PrecoDoM2 { get; set; }
        public double AluguelPorM2 { get; set; }
        public double CapRate { get; set; }
        public double VacanciaMedia { get; set; }
        
    }
}