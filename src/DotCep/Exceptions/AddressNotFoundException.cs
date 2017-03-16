using System;

namespace DotCep.Exceptions
{
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException(string message) : base(message)
        {

        }
    }

}