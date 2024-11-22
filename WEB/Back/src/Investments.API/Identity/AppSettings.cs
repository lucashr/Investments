using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.API.Identity
{
    public class AppSettings
    {
        public string Secret { get; set; } //Segredo
        public int ExpirationInHours { get; set; } //Expiração em Horas
        public string Issuer { get; set; } // Emissor
        public string Audience { get; set; } //Valido Em

    }
}