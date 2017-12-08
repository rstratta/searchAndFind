using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class SalerRequestValidator : AbstractValidator, IRequestValidator<SalerRequest>
    {
        public void ValidateMandatoryFields(SalerRequest request)
        {
            ValidateEmptyField(request.UserId, "Id de vendedor");
            ValidateEmptyField(request.DeviceId, "Dispositivo");
        }

        public void ValidateOptionalFields(SalerRequest request)
        {
            ValidateEmptyField(request.Name, "Nombre");
            ValidateEmptyField(request.LastName, "Apellido");
        }

        public void ValidateRequest(SalerRequest request)
        {
            ValidateMandatoryFields(request);
            ValidateOptionalFields(request);
            ValidateEmptyField(request.Length.ToString(), "Longitud");
            ValidateEmptyField(request.Latitude.ToString(), "Latitud");
            ValidateEmptyField(request.ShopAddress, "Dirección de local");
            ValidateEmptyField(request.ShopName, "Nombre local");
            ValidateEmptyField(request.ShopPhone.ToString(), "Teléfono local");
        }

    }
}
