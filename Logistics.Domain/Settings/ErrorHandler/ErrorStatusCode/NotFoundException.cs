using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Settings.ErrorHandler.ErrorStatusCode
{
    public class NotFoundException:HttpException
    {
        private static readonly HttpStatusCode _statusCode = HttpStatusCode.NotFound;

        public NotFoundException(string message) : base(message, _statusCode)
        {
        }

        public NotFoundException(IList<string> messages) : base(messages, _statusCode)
        {
        }
    }
}
