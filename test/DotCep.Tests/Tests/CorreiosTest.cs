using Xunit;
using DotCep.Services;

namespace DotCep.Tests
{ 
    public class CorreiosTest
    {
        private CorreiosService correiosService;

        public CorreiosTest()
        {
            this.correiosService = new CorreiosService();
        }
        
        [Fact]
        public void Should_Find_Invalid_Cep()
        {
           // var cep = CorreiosService.GetAddressByCep("31510480");

            
        }


    }
}