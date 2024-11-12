namespace BeDesi.Core.Models
{
    public enum ErrorCode
    {
        None = 0,
        ParameterMissing = 1,
        UnhandledError = 2,
        UserMessage = 3,
        CacheTimeOut = 4,
        RequestIdMissing = 5
    }
}
