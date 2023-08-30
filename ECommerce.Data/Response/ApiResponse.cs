using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Response
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request,you have made",
                401 => "Authorized,you are not",
                404 => "Resource found,it was not",
                500 => "Errors are the path to the dark side.Errors lead to anger. Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }

    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ApiResponse(bool success, string message = null, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        

    }
}
