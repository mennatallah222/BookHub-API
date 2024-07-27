using Microsoft.AspNetCore.Http;

namespace API.Core.Features.Authors.Commands.Models
{
    public class CreateAuthor
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
