using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.ViewModels
{
    public class PromotionCreateViewModel
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Subtitle { get; set; }

        public string? Description { get; set; }

        [Required]
        public string CtaLink { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        public int? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? CreatedBy { get; set; }


    }
}
