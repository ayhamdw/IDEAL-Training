﻿namespace ProjectBase.Generic
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public static ApiResponse <T> SuccessResponse (T data, string message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message ?? "Request processed successfully.",
                Data = data,
            };
        }

        public static ApiResponse <T> FailedResponse (string message = null , List<string> errors = null)
        {
            return new ApiResponse<T>()
            {
                Success = false,
                Message = message ,
                Errors = null,
            };
        }

        private ApiResponse()
        {
        }
    }

}
