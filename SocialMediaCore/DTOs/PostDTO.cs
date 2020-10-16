using System;

namespace SocialMediaCore.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
