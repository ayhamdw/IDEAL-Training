using DataEntity.Models;
using DataEntity.ViewModels;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Core.Enums;
using ProjectBase.Services.Interfaces;


namespace ProjectBase.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly ProjectBaseContext _context;

        public PromotionService(ProjectBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsAsync(bool? isActive)
        {
            var query = _context.Promotions.AsQueryable();

            if (isActive.HasValue)
            {
                query = query.Where(p => p.Status == (isActive.Value ?
                    (int)GeneralEnums.StatusEnum.Active :
                    (int)GeneralEnums.StatusEnum.Deactive));
            }

            return await query.ToListAsync();
        }


        public async Task<int> CreatePromotionAsync(PromotionCreateViewModel model, string createdBy)
        {
            var promotion = new Promotion
            {
                Title = model.Title,
                Subtitle = model.Subtitle,
                Description = model.Description,
                CtaLink = model.CtaLink,
                ImageUrl = model.ImageUrl,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
            };

            if (model.Status.HasValue)
            {
                promotion.Status = model.Status.Value;
            }

            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
            return promotion.Id;
        }

    }
}