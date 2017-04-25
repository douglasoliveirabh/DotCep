using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using DotCep.Domain;
using DotCep.Exceptions;
using Newtonsoft.Json;
using System.Text;
using System;

namespace DotCep.Services
{
    public class ViaCepService : ICepService
    {

        private string url = "https://viacep.com.br/ws/{0}/json";

        private string GetFormatUrl(string cep)
        {
            return string.Format(url, cep);
        }

        public async Task<Address> GetAddressByCepAsync(string cep)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(this.GetFormatUrl(cep)))
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                        throw new AddressNotFoundException("Address not Found");

                    using (HttpContent content = response.Content)
                    {
                        dynamic data = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(await content.ReadAsByteArrayAsync()));
                        return await Task.FromResult<Address>(new Address((string)data.cep,
                                                                          (string)data.uf,
                                                                          (string)data.localidade,
                                                                          (string)data.bairro,
                                                                          (string)data.logradouro,
                                                                          eUsedService.ViaCep));
                    }
                }
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

