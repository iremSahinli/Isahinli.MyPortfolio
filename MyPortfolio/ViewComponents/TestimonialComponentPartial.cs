using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Contect;

namespace MyPortfolio.ViewComponents
{
    public class TestimonialComponentPartial : ViewComponent
    {

        private readonly AppDbContext _context;

        public TestimonialComponentPartial(AppDbContext context)
        {
            _context = context;
        }
 
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonials = await _context.Testimonials
                    .OrderByDescending(t => t.SentDate.Date)
                    .Take(4)
                    .ToListAsync();

            return View(testimonials);

        }
    }
}


