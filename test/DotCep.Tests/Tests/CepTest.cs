using Xunit;
using DotCep.Services;

namespace DotCep.Tests
{ 
    public class CepTest
    {
         private CepService CepService;

         public CepTest()
         {
             this.CepService = new CepService();
         }


        [Fact]
        public void Should_Find_Invalid_Cep()
        {
           // var cep = CorreiosService.GetAddressByCep("31510480");

            
        }


    }
}