namespace MyPortfolio.Areas.Admin.Models.AdminAboutVMs;

public class AdminAboutUpdateVM
{
    public int AboutId { get; set; }
    public string Title { get; set; }
    public string SubDescription { get; set; }
    public IFormFile? MyPhoto { get; set; }
    public string? ExistingMyPhoto { get; set; }
}
