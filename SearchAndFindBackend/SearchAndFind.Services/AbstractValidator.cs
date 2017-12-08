using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public abstract class AbstractValidator
    {
        public void ValidateEmptyField(string field, string fieldName)
        {
            if (String.IsNullOrWhiteSpace(field))
            {
                throw new BadRequestException("El campo " + fieldName + " no puede ser vacio");
            }
        }

        public void ValidateNotNullField(Object field, string fieldName)
        {
            if (field == null)
            {
                throw new BadRequestException("El campo " + fieldName + " no puede ser nulo");
            }
        }

        public void ValidateMoreThan(double number, double comparation, string fieldName)
        {
            if (number <= comparation)
            {
                throw new BadRequestException("Verifique el campo " + fieldName);
            }
        }
    }
}
