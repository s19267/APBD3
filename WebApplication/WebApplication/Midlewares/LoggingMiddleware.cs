using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Midlewares
{
    public class LoggingMiddleware
    {
        
        private readonly RequestDelegate _next;
        
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        
        public async Task InvokeAsync(HttpContext httpContext)
        {
            StringBuilder sb=new StringBuilder();
            sb.Append("\nmetoda: "+httpContext.Request.Method.ToString());
            sb.Append("\nsciezka: "+httpContext.Request.Path);
            sb.Append("\nqueryString: "+httpContext.Request.QueryString.ToString());
            string body;
            using (StreamReader reader=new StreamReader(httpContext.Request.Body,Encoding.UTF8,true,1024,true))
            {
                body=await reader.ReadToEndAsync(); 
                
            }
            sb.Append("\nbody: " + body);
           

            using (StreamWriter writer = new StreamWriter("requestsLog.txt", true))
            {
                writer.Write(sb.ToString());
            }
            await _next(httpContext);
        }

    }
}