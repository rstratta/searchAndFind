using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public interface IQueryService
    {
        Response DoQuery(QueryRequest request);
        Response GetPendingQuery(QueryRequest request);
        Response CancelQuery(QueryRequest request);
        Response ConfirmQuery(QueryRequest request);
        Response GetQueryById(QueryRequest request);
    }
}
