using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZ.Data;
using EZ.Domain;

namespace EZ.Data.Contracts
{
    public interface IEzPersonRepository : IRepository<EzPerson>
    {
        IQueryable<EzPerson> GetByPersonId(long id);
        IQueryable<EzPerson> GetByEventId(long id);
        EzPerson GetByIds(long personId, long eventId);
        void Delete(long personId, long eventId);
    }
}
