using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Settings.ErrorHandler.ErrorStatusCode
{
    public class BadRequestException : HttpException
    {
        private static readonly HttpStatusCode _statusCode = HttpStatusCode.BadRequest;

        public BadRequestException(string message) : base(message, _statusCode)
        {
        }

        public BadRequestException(IList<string> messages) : base(messages, _statusCode)
        {
        }
    }
}
