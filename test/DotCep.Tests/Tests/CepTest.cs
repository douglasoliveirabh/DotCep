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

/*
        [Fact]
        public void Should_Find_Invalid_Cep()
        {                
            Assert.ThrowsAsync<AddressNotFoundException>(async () => await cepService.GetAddressByCep("89198198198198"));
        }*/

        [Fact]
        public void Shoud_Find_ValidCep(){
            var t = Task.Run(() => cepService.GetAddressByCep("30150221"));

            if(t.Result != null)
                System.Console.Write(t.Result.ToString());
            else
                System.Console.Write("deu erro");



            Assert.True(t.Result as Address != null);             
        }


    }
}