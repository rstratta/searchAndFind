using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class TenderRequestValidator : AbstractValidator,IRequestValidator<TenderRequest>
    {
        public void ValidateMandatoryFields(TenderRequest request)
        {
            ValidateEmptyField(request.TenderAmount.ToString(),"Monto");
            ValidateMoreThan(request.TenderAmount, 0, "Monto");
        }

        public void ValidateOptionalFields(TenderRequest request)
        {
            ValidateEmptyField(request.TenderId, "Identificador de oferta");
        }

        public void ValidateRequest(TenderRequest request)
        {
            ValidateMandatoryFields(request);
            ValidateOptionalFields(request);
        }
    }
}
