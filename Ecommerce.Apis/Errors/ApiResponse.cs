namespace Ecommerce.Apis.Errors
{
    public class ApiResponse
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse( int? statusCode, string? message = null )
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode( int? statuscode )
        {
            return statuscode switch
            {
                400 => "Bad Request, ya fla7",
                401 => "UnAuthorized, Ya 7ramy",
                404 => "Not Found, etd7k 3lek",
                500 => "Server Error, et3aml m3 el backend",
                _ => null
            };
        }
    }
}