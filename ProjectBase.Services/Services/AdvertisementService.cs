using DataEntity.Models;
using DataEntity.ViewModels;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Services.IServices;

namespace ProjectBase.Services.Services;

public class AdvertisementService : IAdvertisementService
{
    private readonly ProjectBaseContext _context;

    public AdvertisementService(ProjectBaseContext context)
    {
        _context = context;
    }

    public async Task<List<AdvertisementViewModel>> GetActiveAdvertisements()
    {
        var activeAds = await _context.Advertisements
            .Where(a => a.IsActive)
            .OrderBy(a => a.DisplayOrder)
            .Take(3)
            .ToListAsync();

        return activeAds.Select(a => new AdvertisementViewModel(a)).ToList();
    }



    public async Task<int> CreateAdvertisement(AdvertisementViewModel model, string createdBy)
    {
        var ad = new Advertisement
        {
            ImageUrl = model.ImageUrl,
            ImageAltText = model.ImageAltText,
            Label = model.Label,
            TitleText = model.TitleText,
            SubText = model.SubText,
            ButtonText = model.ButtonText,
            ButtonUrl = model.ButtonUrl,
            DisplayOrder = model.DisplayOrder,
            IsActive = model.IsActive,
            CreatedBy = createdBy,
            
        };

        await _context.Advertisements.AddAsync(ad);
        await _context.SaveChangesAsync();
        return ad.Id;
    }

    public async Task<bool> UpdateAdvertisement(AdvertisementUpdateViewModel model)
    {
        var ad = await _context.Advertisements.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (ad == null)
            return false;

        ad.ImageUrl = model.ImageUrl;
        ad.ImageAltText = model.ImageAltText;
        ad.Label = model.Label;
        ad.TitleText = model.TitleText;
        ad.SubText = model.SubText;
        ad.ButtonText = model.ButtonText;
        ad.ButtonUrl = model.ButtonUrl;
        ad.DisplayOrder = model.DisplayOrder;
        ad.IsActive = model.IsActive;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAdvertisement(int id)
    {
        var ad = await _context.Advertisements.FirstOrDefaultAsync(x => x.Id == id);
        if (ad == null)
            return false;

        ad.IsActive = false;

        await _context.SaveChangesAsync();
        return true;
    }
}
