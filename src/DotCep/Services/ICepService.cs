using System.Threading.Tasks;
using DotCep.Domain;

namespace DotCep.Services
{
    public interface ICepService
    {
      Task<Address> GetAddressByCepAsync(string cep);

      Address GetAddressByCep(string cep);

    }

}