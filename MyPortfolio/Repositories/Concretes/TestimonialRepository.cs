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

        public Task<Testimonial> GetTestimonialById(int testimonialId)
        {
            throw new NotImplementedException();
        }
        public Task<Testimonial> DeleteTestimonial(int testimonialId)
        {
            throw new NotImplementedException();
        }

    }
}
