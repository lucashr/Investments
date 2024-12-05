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

        private readonly IEnderecoUsuarioPersist _enderecoUsuarioPersist;

        public EnderecoUsuarioService(IEnderecoUsuarioPersist enderecoUsuarioPersist)
        {
            _enderecoUsuarioPersist = enderecoUsuarioPersist;
        }

        public async Task<EnderecoUsuario> GetAddressUser(string username)
        {
            return await _enderecoUsuarioPersist.GetAddressUser(username);
        }

        public async Task<bool> SaveAddressUser(EnderecoUsuario enderecoUsuario)
        {
            return await _enderecoUsuarioPersist.SaveAddressUser(enderecoUsuario);
        }

        public async Task<bool> UpdateAddressUser(EnderecoUsuario enderecoUsuario)
        {
            return await _enderecoUsuarioPersist.UpdateAddressUser(enderecoUsuario);
        }
    }
}