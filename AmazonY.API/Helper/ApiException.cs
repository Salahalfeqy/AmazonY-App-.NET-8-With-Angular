namespace AmazonY.API.Helper
{
    public class ApiException : ResponseAPI
    {
        public ApiException(int statusCode, string? massage = null ,string details = null) : base(statusCode, massage)
        {
            Details = details;
        }
        public String Details { get; set; }
    }
}
