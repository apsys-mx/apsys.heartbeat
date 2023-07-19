using FluentValidation.Results;
using System.Text.Json;

namespace apsys.heartbeat.exceptions
{
    public class InvalidDomainException : Exception
    {
        private readonly IEnumerable<ValidationFailure> validationFailures;

        public InvalidDomainException(IEnumerable<ValidationFailure> validationFailures)
        {
            this.validationFailures = validationFailures;
        }

        public override string Message
        {
            get
            {
                var messages = from error in this.validationFailures
                               select new { ErrorMessage = error.ErrorMessage, ErrorCode = error.ErrorCode, PropertyName = error.PropertyName };
                return JsonSerializer.Serialize(messages);
            }
        }
    }
}
