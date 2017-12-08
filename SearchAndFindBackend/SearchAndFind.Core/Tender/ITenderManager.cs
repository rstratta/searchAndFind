using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public interface ITenderManager
    {
        TenderDTO DoTender(TenderRequest request);
        TenderResponse GetAceptedTendersBySalerId(string salerId);
        TenderResponse GetAceptedTendersByClientId(string clientId);
        TenderResponse GetTenderById(string tenderId);
        TenderDTO RevokeTender(TenderRequest request);
        TenderDTO ConfirmTender(TenderRequest request);
    }
}
