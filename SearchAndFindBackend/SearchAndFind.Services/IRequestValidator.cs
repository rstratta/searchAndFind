using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public interface IRequestValidator<R>
    {
        void ValidateRequest(R request);
        void ValidateMandatoryFields(R request);
        void ValidateOptionalFields(R request);
    }
}