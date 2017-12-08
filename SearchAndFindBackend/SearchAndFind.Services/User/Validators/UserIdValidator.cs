using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class UserIdValidator : AbstractValidator, IRequestValidator<UserRequest>
    {
        public void ValidateMandatoryFields(UserRequest request)
        {
            ValidateRequest(request);
        }

        public void ValidateOptionalFields(UserRequest request)
        {
            ValidateRequest(request); 
        }

        public void ValidateRequest(UserRequest request)
        {
            ValidateEmptyField(request.UserId, "Id de usuario");
        }
    }
}
