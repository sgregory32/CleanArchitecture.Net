﻿namespace CleanArchitecture.Api.Filters.ErrorHandling
{
    public class ApiError
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string Detail { get; set; }

        public ApiError(string message)
        {
            Message = message;
            IsError = true;
        }
    }
}
