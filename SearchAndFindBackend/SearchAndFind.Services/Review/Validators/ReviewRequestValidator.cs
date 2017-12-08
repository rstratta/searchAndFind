using SearchAndFind.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    public class ReviewRequestValidator : AbstractValidator, IRequestValidator<ReviewRequest>
    {
        public void ValidateMandatoryFields(ReviewRequest request)
        {
            ValidateRequest(request);
        }

        public void ValidateOptionalFields(ReviewRequest request)
        {
            ValidateRequest(request);
        }

        public void ValidateRequest(ReviewRequest request)
        {
            ValidateMoreThan(request.Points, 0, "Puntos");
        }
    }
}
