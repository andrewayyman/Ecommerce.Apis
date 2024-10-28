namespace Ecommerce.Apis.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }


        public ApiValidationErrorResponse() : base(400) // cuz validation error is type of badrequest 
        {

            Errors = new List<string>();
        }



    }
}
