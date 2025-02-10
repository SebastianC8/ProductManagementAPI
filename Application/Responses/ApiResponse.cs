using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public T Data { get; set; }

        public List<string> Errors { get; set; }


        // Constructor for successful responses
        public ApiResponse(int statusCode, string message, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Errors = new List<string>();
        }

        // Constructor for error responses

        public ApiResponse(int statusCode, string message, List<string> errors)
        {
            StatusCode = statusCode;
            Message = message;
            Data = default;
            Errors = errors;
        }

    }
}
