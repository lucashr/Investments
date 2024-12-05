using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;

namespace Investments.Persistence.Contracts
{
    public interface IEnderecoUsuarioPersist
    {
        Task<EnderecoUsuario> GetAddressUser(string username);
        Task<bool> SaveAddressUser(EnderecoUsuario enderecoUsuario);
        Task<bool> UpdateAddressUser(EnderecoUsuario enderecoUsuario);
    }
}