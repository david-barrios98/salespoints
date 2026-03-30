using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salespoints.Application.DTOs.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsException { get; set; }

        public static ApiResponse<T> Success(T data, string message = "")
            => new() { IsSuccess = true, Data = data, Message = message };

        public static ApiResponse<T> Error(string message)
            => new() { IsSuccess = false, Message = message };

        public static ApiResponse<T> Exception(string message)
            => new() { IsSuccess = false, IsException = true, Message = message };
    }
}
