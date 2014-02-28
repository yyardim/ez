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
    public class EzPersonRepository : EzRepository<EzPerson>, IEzPersonRepository
    {
        public EzPersonRepository(DbContext context) : base(context) { }

        public override EzPerson GetById(long id)
        {
            throw new InvalidOperationException("Cannot return with one id");
        }
        public override EzPerson GetById(string id)
        {
            throw new InvalidOperationException("Cannot return with one id");
        }
        public EzPerson GetByIds(long personId, long eventId)
        {
            return DbSet.FirstOrDefault(p => p.PersonId == personId && p.EzId == eventId);
        }

        public IQueryable<EzPerson> GetByPersonId(long id)
        {
            return DbSet.Where(ep => ep.PersonId == id);
        }

        public IQueryable<EzPerson> GetByEventId(long id)
        {
            return DbSet.Where(ep => ep.EzId == id);
        }

        public override void Delete(long id)
        {
            throw new InvalidOperationException("Cannot delete an EzPersons object with one id");
        }

        public void Delete(long personId, long eventId)
        {
            var ezPerson = new EzPerson { PersonId = personId, EzId = eventId };
            Delete(ezPerson);
        }
    }
}
