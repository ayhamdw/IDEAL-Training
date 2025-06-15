namespace DataEntity.Models;

    public class Advertisement
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public string Label { get; set; }
        public string TitleText { get; set; }
        public string SubText { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

