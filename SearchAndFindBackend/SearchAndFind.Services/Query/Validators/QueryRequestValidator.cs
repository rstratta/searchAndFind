using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class QueryRequestValidator : AbstractValidator, IRequestValidator<QueryRequest>
    {
        public virtual void ValidateMandatoryFields(QueryRequest request)
        {
            ValidateEmptyField(request.CurrentToken, "Clave de autenticación");
            ValidateEmptyField(request.UserId, "Id de usuario");
        }

        public void ValidateOptionalFields(QueryRequest request)
        {
            ValidateEmptyField(request.Category, "Categoría");
        }

        public void ValidateRequest(QueryRequest request)
        {
            ValidateMandatoryFields(request);
            ValidateOptionalFields(request);
        }
    }
}
