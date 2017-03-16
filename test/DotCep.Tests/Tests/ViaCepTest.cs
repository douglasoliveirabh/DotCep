using Xunit;
using DotCep.Services;
using System.Threading.Tasks;
using System;
using DotCep.Exceptions;

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
            var task = Task.Run(() => viacepService.GetAddressByCep("31510480"));              
        }



    }
}