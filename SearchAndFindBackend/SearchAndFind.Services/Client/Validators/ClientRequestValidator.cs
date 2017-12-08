using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class ClientRequestValidator : AbstractValidator, IRequestValidator<ClientRequest>
    {
        public void ValidateMandatoryFields(ClientRequest request)
        {
            ValidateEmptyField(request.Name, "Nombre");
            ValidateEmptyField(request.LastName, "Apellido");
        }

        public void ValidateOptionalFields(ClientRequest request)
        {
            ValidateEmptyField(request.UserId, "Id de cliente");
            ValidateEmptyField(request.DeviceId, "Dispositivo");
        }

        public void ValidateRequest(ClientRequest request)
        {
            ValidateMandatoryFields(request);
            ValidateOptionalFields(request);
        }
    }
}
