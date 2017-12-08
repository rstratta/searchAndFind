using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class SalerCategoryRequestValidtor : AbstractValidator, IRequestValidator<SalerRequest>
    {
        public void ValidateMandatoryFields(SalerRequest request)
        {
            ValidateRequest(request);
        }

        public void ValidateOptionalFields(SalerRequest request)
        {
            ValidateRequest(request);
        }

        public void ValidateRequest(SalerRequest request)
        {
            ValidateEmptyField(request.UserId, "Id de Saler");
        }
    }
}
