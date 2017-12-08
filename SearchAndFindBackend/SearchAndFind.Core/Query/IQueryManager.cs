using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface IQueryManager
    {
        QueryDTO DoQuery(QueryRequest request);
        void CancelQuery(QueryRequest request);
        void ConfirmQuery(QueryRequest request);
        QueryDTO GetCurrentQuery(QueryRequest request);
        QueryDTO GetQueryById(string queryId);
    }
}
