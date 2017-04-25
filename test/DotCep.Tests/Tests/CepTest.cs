using Xunit;
using DotCep.Services;
using DotCep.Exceptions;
using System.Threading.Tasks;
using DotCep.Domain;

namespace DotCep.Tests
{ 
    public class CepTest
    {
         private CepService cepService;

         public CepTest()
         {
             this.cepService = new CepService();
         }


        [Fact]
        public void Should_Find_Invalid_Cep()
        {                
            Assert.ThrowsAsync<AddressNotFoundException>(async () => await cepService.GetAddressByCepAsync("89198198198198"));
        }

        [Fact]
        public void Shoud_Find_ValidCep(){
            var t = Task.Run(() => cepService.GetAddressByCepAsync("30150221"));
            Assert.True(t.Result as Address != null);             
        }


    }
}