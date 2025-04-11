namespace MyPortfolio.Areas.Admin.Models.AdminTestimonialVM
{
    public class AdminTestimonialVM
    {
        public int TestimonialId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
    }
}
