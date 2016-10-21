using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Infrastructure
{
    public class HttpContextFactory : IHttpContextFactory
    {
        public HttpContextBase Create()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }
    }
}