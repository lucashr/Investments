using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;

namespace Investments.Application.Contracts
{
    public interface IEnderecoUsuarioService
    {
        Task<EnderecoUsuario> GetAddressUser(string username);
        Task<bool> SaveAddressUser(EnderecoUsuario enderecoUsuario);
        Task<bool> UpdateAddressUser(EnderecoUsuario enderecoUsuario);
    }
}