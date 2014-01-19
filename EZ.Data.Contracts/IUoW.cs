using EZ.Data.Contracts;

namespace EZ.Data
{
    public interface IUoW
    {
        void Commit();

        //IRepository<Ez> Ezs { get; }
        //IRepository<Person> People { get; }
        //IRepository<Address> Addresses { get; }
        IEzRepository Ezs { get; }
        IPersonRepository Persons { get; }
        IAddressRepository Addresses { get; }
        ICategoryRepository Categories { get ; }
        IEventerRepository Eventers { get; }
        IPersonAddressRepository PersonAddresses { get; }
    }
}
