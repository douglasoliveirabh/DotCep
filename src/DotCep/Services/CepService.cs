using System.Threading.Tasks;
using DotCep.Domain;
using DotCep.Exceptions;
using System.Collections.Generic;
using System;

namespace DotCep.Services
{
    public class CepService : ICepService
    {

        private CorreiosService CorrService;
        private ViaCepService VcService;


        public CepService()
        {
            this.CorrService = new CorreiosService();
            this.VcService = new ViaCepService();
        }
        
        public async Task<Address> GetAddressByCepAsync(string cep)
        {
            try
            {
                var cepTasks = new List<Task<Address>>(){
                    this.CorrService.GetAddressByCepAsync(cep),
                    this.VcService.GetAddressByCepAsync(cep)
                };

                var returnTask = await Task<Address>.WhenAny(cepTasks.ToArray());

                return await returnTask;

            }
            catch (AddressNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }        

        public Address GetAddressByCep(string cep)
        {
            throw new NotImplementedException();
        }
    }
}

