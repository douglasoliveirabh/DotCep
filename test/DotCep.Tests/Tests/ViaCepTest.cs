using Xunit;
using DotCep.Services;
using System.Threading.Tasks;
using DotCep.Exceptions;
using DotCep.Domain;

namespace DotCep.Tests
{
    public class ViaCepTest
    {
        private ViaCepService viacepService;

        public ViaCepTest()
        {
            this.viacepService = new ViaCepService();
        }


        [Fact]
        public void Should_Find_Invalid_Cep()
        {                
            Assert.ThrowsAsync<AddressNotFoundException>(async () => await viacepService.GetAddressByCep("89198198198198"));
        }

        [Fact]
        public void Shoud_Find_ValidCep(){
            var t = Task.Run(() => viacepService.GetAddressByCep("31510480"));
            Assert.True(t.Result as Address != null);             
        }



    }
}