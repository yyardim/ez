using EZ.Data;
using EZ.Data.Contracts;
using EZ.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Data
{
    public class PersonAddressRepository : EZRepository<PersonAddress>, IPersonAddressRepository
    {
        public PersonAddressRepository(DbContext context) : base(context) { }
        public override PersonAddress GetById(long id)
        {
            throw new InvalidOperationException("Cannot return with one id");
        }
        public override PersonAddress GetById(string id)
        {
            throw new InvalidOperationException("Cannot return with one id");
        }
        public IQueryable<PersonAddress> GetByPersonID(long id)
        {
            return DbSet.Where(p => p.PersonId == id);
        }

        public IQueryable<PersonAddress> GetByAddressID(long id)
        {
            return DbSet.Where(p => p.AddressId == id);
        }

        public PersonAddress GetByIds(long personId, long addressId)
        {
            return DbSet.FirstOrDefault(p => p.PersonId == personId && p.AddressId == addressId);
        }
        public override void Delete(long id)
        {
            throw new InvalidOperationException("Cannot delete a PersonAddress object with one id");
        }
        public void Delete(long personId, long addressId)
        {
            var personAddress = new PersonAddress { PersonId = personId, AddressId = addressId };
            Delete(personAddress);
        }
    }
}
