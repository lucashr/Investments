using Investments.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Investments.Domain
{
    public class EnderecoUsuario : IdentityUser<string>
    {
        public string UserId { get; set; }
        public string cep { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        // Navegação inversa
        public User User { get; set; }

    }
}