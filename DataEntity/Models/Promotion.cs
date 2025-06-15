using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataEntity.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string CtaLink { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Status { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public string? CreatedBy { get; set; }
    }
}
