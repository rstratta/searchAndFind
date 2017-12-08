using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface IQueryRepository : IRepository<Query>
    {
        Query GetCurrentQueryByClientId(Guid clientId);
        Query GetFullQuery(Guid queryId);
    }
}
