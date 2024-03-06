﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Settings.ErrorHandler
{
    public abstract class HttpException: Exception
    {
        public HttpStatusCode StatusCode { get; protected set; }
        public IList<string> Errors { get; protected set; }

        public HttpException(string message, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Errors = new string[] { message };
        }

        public HttpException(IList<string> messages, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Errors = messages;
        }
    }
}
