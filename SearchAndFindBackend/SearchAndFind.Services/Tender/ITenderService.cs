using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public interface ITenderService
    {
        Response DoTender(TenderRequest request);
        Response GetAllTendersBySaler(TenderRequest request);
        Response GetAllTenderByClient(TenderRequest request);
        Response GetTenderById(TenderRequest request);
        Response ConfirmTender(TenderRequest request);
        Response RevokeTender(TenderRequest request);
    }
}
