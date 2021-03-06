﻿using System;
using System.Net;

namespace com.b_velop.Deploy_O_Mat.Web.Common.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; }

        public RestException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}