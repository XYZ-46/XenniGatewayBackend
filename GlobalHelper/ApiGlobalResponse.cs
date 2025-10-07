using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalHelper
{
    public class ApiGlobalResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public Dictionary<string, object> Metadata { get; set; }

        public ApiGlobalResponse(T data, string message = "Success", Dictionary<string, object> metadata = null)
        {
            IsSuccess = true;
            Message = message;
            Data = data;
            Metadata = metadata;
        }

        // This constructor can be used for explicit failures, though global exception handling is preferred
        public ApiGlobalResponse(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }
}
