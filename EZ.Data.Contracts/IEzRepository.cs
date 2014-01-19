using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZ.Data;
using EZ.Domain;

namespace EZ.Data.Contracts
{
    public interface IEzRepository : IRepository<Ez>
    {
    }
}
