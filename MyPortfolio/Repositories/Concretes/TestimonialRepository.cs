using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Contect;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.Repositories.Concretes
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly AppDbContext _context;

        public TestimonialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Testimonial>> GetAllTestimonialsAsync()
        {
            return await _context.Testimonials.ToListAsync();
        }

        public async Task<Testimonial> GetTestimonialId(int testimonialId)
        {
            var testimonial = await _context.Testimonials.FindAsync(testimonialId);
            return testimonial;
        }
        public async Task<Testimonial> DeleteTestimonial(int testimonialId)
        {
            try
            {
                var testimonial = await _context.Testimonials.FirstOrDefaultAsync(t => t.TestimonialId == testimonialId);
                if (testimonial != null)
                {
                    _context.Testimonials.Remove(testimonial);
                    await _context.SaveChangesAsync();
                }
                return testimonial;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
