using EZ.Data.Contracts;

namespace EZ.Data
{
    public interface IEzUow
    {
        void Commit();

        //IRepository<Ez> Ezs { get; }
        //IRepository<Person> People { get; }
        //IRepository<Address> Addresses { get; }
        IEzRepository Ezs { get; }
        IPersonRepository Persons { get; }
        IAddressRepository Addresses { get; }
        ICategoryRepository Categories { get ; }
        IEzPersonRepository EzPersons { get; }
        IPersonAddressRepository PersonAddresses { get; }
    }
}
