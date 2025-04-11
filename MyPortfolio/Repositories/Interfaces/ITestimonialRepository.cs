using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Repositories.Interfaces
{
    public interface ITestimonialRepository
    {
        Task<IEnumerable<Testimonial>> GetAllTestimonialsAsync();
        Task<Testimonial> GetTestimonialId(int testimonialId);
        Task<Testimonial> DeleteTestimonial(int testimonialId);
        Task<int> GetUnreadTestimonialsAsync();
    }
}
