using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using DotCep.Domain;
using DotCep.Exceptions;
using System.Linq;
using System;

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

        public async Task<Address> GetAddressByCepAsync(string cep)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.PostAsync(url, new StringContent(GetFormatBody(cep))))
                {
                    //throw new Exceptions.AddressNotFoundException("");

                    if (response.StatusCode == HttpStatusCode.BadRequest || 
                        response.StatusCode == HttpStatusCode.InternalServerError)
                        throw new AddressNotFoundException("Address not Found");

                    using (HttpContent content = response.Content)
                    {                        
                        XDocument correiosResponse = XDocument.Load(await content.ReadAsStreamAsync());                        
                        var data = (from c in correiosResponse.Descendants()
                                    where c.Name == "return"
                                    select c).SingleOrDefault();                        

                        return await Task.FromResult<Address>(new Address(data.Element(XName.Get("cep")).Value,
                                                                          data.Element(XName.Get("uf")).Value,
                                                                          data.Element(XName.Get("cidade")).Value,
                                                                          data.Element(XName.Get("bairro")).Value,
                                                                          data.Element(XName.Get("end")).Value,
                                                                          eUsedService.Correios));
                                                                          
                    }
                }
            }
            catch (AddressNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
        } 


       public Address GetAddressByCep(string cep)
        {
            throw new NotImplementedException();
        }
      
    }
}
