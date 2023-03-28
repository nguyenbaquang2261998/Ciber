using System.ComponentModel.DataAnnotations;

namespace Ciber.Entity
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
