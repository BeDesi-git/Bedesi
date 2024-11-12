namespace BeDesi.Core.Models
{
    public class ResponseFactory
    {
        public static ApiResponse<T> CreateResponse<T>(T result)
        {
            return new ApiResponse<T>(result);
        }

        public static ApiResponse<T> CreateFailedResponse<T>(ErrorCode erroCode, string errorMessage)
        {
            return new ApiResponse<T>(erroCode, errorMessage, "No Error Detail");
        }

        public static ApiResponse<T> CreateFailedResponse<T>(ErrorCode erroCode, string errorMessage, string errorDetail)
        {
            return new ApiResponse<T>(erroCode, errorMessage, errorDetail);
        }

    }
}
