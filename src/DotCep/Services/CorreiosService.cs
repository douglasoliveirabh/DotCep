using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotCep.Domain;

namespace DotCep.Services
{
    public class CorreiosService : ICepService
    {
        public Task<Address> GetAddressByCep(string cep)
        {
            return Task.FromResult<Address>(new Address(cep,"","","","", eServiceUsed.Correios));
        }
    }
}
