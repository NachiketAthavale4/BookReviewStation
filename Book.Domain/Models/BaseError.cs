namespace Book.Domain.Models
{
    public class BaseError
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

        public BaseError()
        {

        }

        public BaseError(string errorDescription, string errorCode)
        {
            ErrorDescription = errorDescription;
            ErrorCode = errorCode;
        }
    }
}