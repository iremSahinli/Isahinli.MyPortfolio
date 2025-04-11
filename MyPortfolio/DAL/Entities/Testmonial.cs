using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.DAL.Entities
{
    public class Testimonial
    {
        [Key]
        public int TestimonialId { get; set; }
        public string FullName { get; set; }
        public DateTime SentDate { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
    }
}
