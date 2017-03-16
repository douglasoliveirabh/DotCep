namespace DotCep.Domain
{
    public enum eServiceUsed
    {
        Correios = 0,
        ViaCep = 1
    }

    public class Address
    {
        public string Cep { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Neighborhood { get; private set; }
        public string Street { get; private set; }

        public eServiceUsed ServiceUsed { get; private set; }

        public Address(string cep, string state, string city, string neighborhood, string street, eServiceUsed serviceUsed)
        {
            this.Cep = cep;
            this.State = state;
            this.City = city;
            this.Neighborhood = neighborhood;
            this.Street = street;
            this.ServiceUsed = serviceUsed;
        }

        public override string ToString()
        {

            return string.Format(@"
                    Cep: {0}
                    State: {1}
                    City: {2}
                    Neighborhood: {3}
                    Street : {4}
                    ServiceUsed : {5}",
                    this.Cep,
                    this.State,
                    this.City,
                    this.Neighborhood,
                    this.Street,
                    this.ServiceUsed.ToString());


        }
    }
}