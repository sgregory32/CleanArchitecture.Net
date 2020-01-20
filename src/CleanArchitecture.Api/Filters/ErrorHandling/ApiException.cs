using System;

namespace CleanArchitecture.Api.Filters.ErrorHandling
{
    public class ApiException 
    {
        public int StatusCode { get; set; }
         public string Message { get; }

        public ApiException(Exception ex, int statusCode = 500)
        {
            StatusCode = statusCode;
            Message = ex.InnerException.ToString();       
        }
    }
}
