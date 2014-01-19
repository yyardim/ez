using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZ.Data;
using EZ.Domain;

namespace EZ.Data.Contracts
{
    public interface IEventerRepository : IRepository<Eventer>
    {
        IQueryable<Eventer> GetByPersonId(long id);
        IQueryable<Eventer> GetByEventId(long id);
        Eventer GetByIds(long personId, long eventId);
        void Delete(long personId, long eventId);
    }
}
