using Microsoft.AspNetCore.Http;

namespace Richard2.Models
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
    }
}
