using Microsoft.AspNetCore.Http;

namespace SorterServices.Models
{
    public class ImageViewModel
    {
        public IFormFile Image { get; set; }
    }
}
