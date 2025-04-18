namespace MyPortfolio.Areas.Admin.Models.AdminAboutVMs;

public class AdminAboutListVM
{
    public int AboutId { get; set; }
    public string Title { get; set; }
    public string SubDescription { get; set; }
    public byte[]? MyPhoto { get; set; }
}
