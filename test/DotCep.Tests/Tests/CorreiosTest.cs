using Xunit;
using DotCep.Services;
using System.Threading.Tasks;
using DotCep.Domain;
using DotCep.Exceptions;

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
            Assert.ThrowsAsync<AddressNotFoundException>(async () => await correiosService.GetAddressByCep("89198198198198"));
        }

        [Fact]
        public void Shoud_Find_ValidCep(){
            var t = Task.Run(() => correiosService.GetAddressByCep("30150221"));
            Assert.True(t.Result as Address != null);             
        }

    }
}