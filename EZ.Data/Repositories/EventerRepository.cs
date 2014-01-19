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
    public class EventerRepository : EZRepository<Eventer>, IEventerRepository
    {
        public EventerRepository(DbContext context) : base(context) { }

        public override Eventer GetById(long id)
        {
            throw new InvalidOperationException("Cannot return with one id");
        }
        public override Eventer GetById(string id)
        {
            throw new InvalidOperationException("Cannot return with one id");
        }
        public Eventer GetByIds(long personId, long eventId)
        {
            return DbSet.FirstOrDefault(p => p.PersonId == personId && p.EzId == eventId);
        }

        public IQueryable<Eventer> GetByPersonId(long id)
        {
            return DbSet.Where(ep => ep.PersonId == id);
        }

        public IQueryable<Eventer> GetByEventId(long id)
        {
            return DbSet.Where(ep => ep.EzId == id);
        }

        public override void Delete(long id)
        {
            throw new InvalidOperationException("Cannot delete an Eventers object with one id");
        }

        public void Delete(long personId, long eventId)
        {
            var eventer = new Eventer { PersonId = personId, EzId = eventId };
            Delete(eventer);
        }
    }
}
