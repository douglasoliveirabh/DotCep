using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using DotCep.Domain;
using DotCep.Exceptions;
using System;
using Newtonsoft.Json;
using System.Text;

namespace DotCep.Services
{
    public class ViaCepService : ICepService
    {

        private string url = "https://viacep.com.br/ws/{0}/json/";

        private string GetFormatUrl(string cep)
        {
            return string.Format(url, cep);
        }

        public async Task<Address> GetAddressByCep(string cep)
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
                                                                          eServiceUsed.ViaCep));
                    }
                }
            }
            catch (AddressNotFoundException ex)
            {
                throw ex;
            }
            catch (System.Exception)
            {
                throw;
            }

        }


    }
}

