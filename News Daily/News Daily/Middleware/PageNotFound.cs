using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Daily.Middleware
{
    public class PageNotFound
    {
        private readonly RequestDelegate next;
        public PageNotFound(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await this.next.Invoke(httpContext);


            if (httpContext.Response.StatusCode == 404)
            {
                httpContext.Response.Redirect("/Home/PageNotFound");
            }

        }
    }
}
