using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.ViewComponents;

public class SidebarMessageViewComponent : ViewComponent
{
    private readonly ITestimonialRepository _testimonialRepository;

    public SidebarMessageViewComponent(ITestimonialRepository testimonialRepository)
    {
        _testimonialRepository = testimonialRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var unreadTestimonialsCount = await _testimonialRepository.GetUnreadTestimonialsAsync();
        ViewBag.UnreadTestimonialsCount = unreadTestimonialsCount;
        return View("Default", unreadTestimonialsCount);
    }
}
