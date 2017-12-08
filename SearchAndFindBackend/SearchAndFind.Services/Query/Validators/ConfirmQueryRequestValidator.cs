using SearchAndFind.DTO;

namespace SearchAndFind.Services
{
    public class ConfirmQueryRequestValidator : QueryRequestValidator
    {

        public override void ValidateMandatoryFields(QueryRequest request)
        {
            ValidateEmptyField(request.CurrentToken, "Clave de autenticación");
            ValidateEmptyField(request.UserId, "Id de usuario");
            ValidateEmptyField(request.TenderConfirmId, "Id de oferta confirmada");
        }
    }
}
