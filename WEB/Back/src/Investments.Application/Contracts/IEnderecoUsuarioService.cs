using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;

namespace Investments.Application.Contracts
{
    public interface IEnderecoUsuarioService
    {
        Task<UserAddress> GetAddressUser(string username);
        Task<bool> SaveAddressUser(UserAddress enderecoUsuario);
        Task<bool> UpdateAddressUser(UserAddress enderecoUsuario);
    }
}