using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using DotCep.Domain;
using DotCep.Exceptions;
using System.Text;

namespace DotCep.Services
{
    public class CorreiosService : ICepService
    {
        private const string url = "https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente";
        private const string body = @"<?xml version=""1.0""?>
                                      <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" 
                                                        xmlns:cli=""http://cliente.bean.master.sigep.bsb.correios.com.br/"">
                                      <soapenv:Header />
                                        <soapenv:Body>
                                            <cli:consultaCEP>
                                                <cep>{0}</cep>
                                            </cli:consultaCEP>
                                        </soapenv:Body>
                                      </soapenv:Envelope>";

        private string GetFormatBody(string cep)
        {
            return string.Format(body, cep);
        }

        public async Task<Address> GetAddressByCep(string cep)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.PostAsync(url, new StringContent(GetFormatBody(cep))))
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                        throw new AddressNotFoundException("Address not Found");

                    using (HttpContent content = response.Content)
                    {
                        //dynamic data = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(await content.ReadAsByteArrayAsync()));                        

                        XDocument correiosResponse = XDocument.Load(await content.ReadAsStreamAsync());
                        var cepResponse = correiosResponse.Descendants("Body").Descendants("consultaCEPResponse");
                        var cepData = cepResponse.Descendants("return");

                        var nodes = cepData.Nodes();
                        

                        System.Console.Write();



                        return await Task.FromResult<Address>(new Address("", "", "", "", "", eUsedService.Correios));

                        /*return await Task.FromResult<Address>(new Address((string)data.cep,
                                                                          (string)data.uf,
                                                                          (string)data.localidade,
                                                                          (string)data.bairro,
                                                                          (string)data.logradouro,
                                                                          eUsedService.Correios));
                                                                          */
                    }
                }
            }
            catch (AddressNotFoundException ex)
            {
                throw ex;
            }
            catch (System.Exception ex2)
            {
                throw;
            }
        }       
    }
}
