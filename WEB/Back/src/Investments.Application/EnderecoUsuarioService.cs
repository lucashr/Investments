using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Domain;
using Investments.Persistence.Contracts;

namespace Investments.Application
{
    public class EnderecoUsuarioService : IEnderecoUsuarioService
    {

        private readonly IUserAddressPersist _enderecoUsuarioPersist;

        public EnderecoUsuarioService(IUserAddressPersist enderecoUsuarioPersist)
        {
            _enderecoUsuarioPersist = enderecoUsuarioPersist;
        }

        public async Task<UserAddress> GetAddressUser(string username)
        {
            return await _enderecoUsuarioPersist.GetAddressUser(username);
        }

        public async Task<bool> SaveAddressUser(UserAddress enderecoUsuario)
        {
            return await _enderecoUsuarioPersist.SaveAddressUser(enderecoUsuario);
        }

        public async Task<bool> UpdateAddressUser(UserAddress enderecoUsuario)
        {
            return await _enderecoUsuarioPersist.UpdateAddressUser(enderecoUsuario);
        }
    }
}