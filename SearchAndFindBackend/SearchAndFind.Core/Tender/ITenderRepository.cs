using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface ITenderRepository : IRepository<Tender>
    {
        ICollection<Tender> GetAceptedTendersBySalerId(Guid salerId);
        ICollection<Tender> GetAceptedTendersByClientId(Guid clientId);
        Tender GetTenderBySalerIdAndQueryId(Guid salerId, Guid queryId);
        ICollection<Tender> GetTendersByQueryId(Guid queryId);
    }
}
