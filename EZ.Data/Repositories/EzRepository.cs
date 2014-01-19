using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZ.Data;
using EZ.Data.Contracts;
using EZ.Domain;
using System.Data.Entity;

namespace EZ.Data
{
    public class EzRepository : EZRepository<Ez>, IEzRepository
    {
        public EzRepository(DbContext context) : base(context) { }
    }
}
