using EZ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Data.Contracts
{
    public interface IPersonAddressRepository : IRepository<PersonAddress>
    {
        IQueryable<PersonAddress> GetByPersonID(long id);
        IQueryable<PersonAddress> GetByAddressID(long id);
        PersonAddress GetByIds(long personId, long addressId);
        void Delete(long personId, long addressId);
    }
}
