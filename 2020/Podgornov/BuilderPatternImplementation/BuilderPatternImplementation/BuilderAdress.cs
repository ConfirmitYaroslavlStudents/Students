

namespace BuilderPatternImplementation
{
    class AddressBuilder
    {
        private readonly Address _address;

        public AddressBuilder()
        {
            _address = new Address();
        }

        private AddressBuilder(AddressBuilder addressBuilder)
        {
            _address = new Address()
            {
                City = addressBuilder._address.City,
                Street = addressBuilder._address.Street,
                House = addressBuilder._address.House,
                Building = addressBuilder._address.Building,
                Flat = addressBuilder._address.Flat
            };
        }

        public AddressBuilder AddCity(string city)
        {
            var newBuilderAddress =  new AddressBuilder(this);
            newBuilderAddress._address.City = city;
            return newBuilderAddress;
        }

        public AddressBuilder AddStreet(string street)
        {
            var newBuilderAddress = new AddressBuilder(this);
            newBuilderAddress._address.Street = street;
            return newBuilderAddress;
        }

        public AddressBuilder AddHouse(int house)
        {
            var newBuilderAddress = new AddressBuilder(this);
            newBuilderAddress._address.House = house;
            return newBuilderAddress;
        }

        public AddressBuilder AddBuilding(int builder)
        {
            var newBuilderAddress = new AddressBuilder(this);
            newBuilderAddress._address.Building = builder;
            return newBuilderAddress;
        }

        public AddressBuilder AddFlat(int flat)
        {
            var newBuilderAddress = new AddressBuilder(this);
            newBuilderAddress._address.Flat = flat;
            return newBuilderAddress;
        }

        public Address Build() => _address;

    }
}
