namespace BeDesi.Core.Models
{
    public class ApiResponse<T> : RequestId
    {
        public bool HasError { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public T Result { get; set; }

        public ApiResponse(ErrorCode erroCode, string errorMessage, string errorDetail)
        {
            HasError = true;
            ErrorCode = erroCode;
            ErrorMessage = errorMessage;
            ErrorDetail = errorDetail;
        }

        public ApiResponse(T result)
        {
            HasError = false;
            Result = result;
        }
    }
}
