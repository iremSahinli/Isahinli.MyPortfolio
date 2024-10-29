namespace MyPortfolio.Areas.Admin.Models
{
    public class AdminMessageVM
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsMessageRead { get; set; }
    }
}
