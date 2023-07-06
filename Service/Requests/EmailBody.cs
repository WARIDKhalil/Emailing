using Microsoft.AspNetCore.Http;

namespace Service.Requests
{
    public class EmailBody
    {
        public string Message { get; set; }
        public IEnumerable<IFormFile>? Attachements { get; set; }
    }
}
