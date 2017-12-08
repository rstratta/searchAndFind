using SearchAndFind.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.Entities;
using SearchAndFind.DataAccess;
using System.Data.Entity;
using log4net;

namespace SearchAndFind.Repository
{
    public class TenderRepository : AbstractRepository<Tender>, ITenderRepository
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override Tender GetById(Guid id)
        {
            
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var tenderResult = from tender in db.Tenders.Include("Images") where tender.Id.Equals(id) select tender;
                    return tenderResult.Single();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting tender by id: ", e);
                    throw new RepositoryException("Error al obtener ofertas de cliente", e);
                }
            
        }
        public ICollection<Tender> GetAceptedTendersByClientId(Guid clientId)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var tenderResult = from tender in db.Tenders.Include("Images") where tender.ClientId.Equals(clientId) && tender.State.Equals(Tender.ACEPT_TENDER) select tender;
                    return tenderResult.ToList();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting accepted tender by client: ", e);
                    throw new RepositoryException("Error al obtener ofertas de cliente",e);
                }
        }

        public ICollection<Tender> GetAceptedTendersBySalerId(Guid salerId)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var tenderResult = from tender in db.Tenders.Include(i => i.Images) where tender.SalerId.Equals(salerId) && tender.State.Equals(Tender.ACEPT_TENDER) select tender;
                    return tenderResult.ToList();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting accepted tender by saler: ", e);
                    throw new RepositoryException("Error al obtener ofertas de vendedor",e);
                }
        }

        public Tender GetTenderBySalerIdAndQueryId(Guid salerId, Guid queryId)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var tenderResult = from tender in db.Tenders where tender.SalerId.Equals(salerId) && tender.QueryId.Equals(queryId) select tender;
                    return tenderResult.First();
                }
                catch (Exception e)
                {
                    throw new RepositoryException("Error al obtener oferta", e);
                }
        }

        public ICollection<Tender> GetTendersByQueryId(Guid queryId)
        {
            using (var db = new SearchAndFindDbContext())
                try
                {
                    var tenderResult = from tender in db.Tenders where tender.QueryId.Equals(queryId)  select tender;
                    return tenderResult.ToList();
                }
                catch (Exception e)
                {
                    logger.Error("Error getting tender by queryId: ", e);
                    throw new RepositoryException("Error al obtener ofertas por consulta", e);
                }
        }
    }
}
