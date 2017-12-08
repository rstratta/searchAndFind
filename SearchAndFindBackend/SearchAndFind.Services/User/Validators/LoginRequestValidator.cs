using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class LoginRequestValidator : AbstractValidator, IRequestValidator<UserRequest>
    {
        private static int MIN_LENGTH = 5;
        public void ValidateMandatoryFields(UserRequest request)
        {
            ValidateEmptyField(request.Mail, "Mail");
            ValidateEmptyField(request.Password, "Password");
            ValidateFormatMail(request.Mail);
            ValidateStrongPassword(request.Password);
        }

        private void ValidateFormatMail(string mail)
        {
            //TODO view a rex to solve it
            if (!mail.Contains("@"))
            {
                throw new BadRequestException("Verifique el correo ingresado");
            }
        }
        private void ValidateStrongPassword(string password)
        {
            if (password.Length <= MIN_LENGTH)
            {
                throw new BadRequestException("La password no cumple el largo mínimo");
            }
        }

        public void ValidateOptionalFields(UserRequest request)
        {
            ValidateMandatoryFields(request);
        }

        public void ValidateRequest(UserRequest request)
        {
            ValidateMandatoryFields(request);
        }
    }
}
