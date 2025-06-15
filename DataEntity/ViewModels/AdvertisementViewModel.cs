using DataEntity.Models;

namespace DataEntity.ViewModels
{
    public class AdvertisementViewModel
    {
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public string Label { get; set; }
        public string TitleText { get; set; }
        public string SubText { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public AdvertisementViewModel(Advertisement advertisement)
        {
            ArgumentNullException.ThrowIfNull(advertisement);

            ImageUrl = advertisement.ImageUrl;
            ImageAltText = advertisement.ImageAltText;
            Label = advertisement.Label;
            TitleText = advertisement.TitleText;
            SubText = advertisement.SubText;
            ButtonText = advertisement.ButtonText;
            ButtonUrl = advertisement.ButtonUrl;
            DisplayOrder = advertisement.DisplayOrder;
            IsActive = advertisement.IsActive;
        }
    }

    public class AdvertisementUpdateViewModel
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
    }

    public class AdvertisementDeleteViewModel
    {
        public int Id { get; set; }  
    }

    
    }