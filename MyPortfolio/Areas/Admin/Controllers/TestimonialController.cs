using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Areas.Admin.Models;
using MyPortfolio.DAL.Contect;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialRepository _testimonialRepository;

        public TestimonialController(ITestimonialRepository testimonialRepository)
        {
            _testimonialRepository = testimonialRepository;
        }

        public async Task<IActionResult> Index()
        {
            var testimonials = await _testimonialRepository.GetAllTestimonialsAsync();
            var testimonialVm = testimonials.Select(model => new AdminTestimonialVM
            {
                TestimonialId = model.TestimonialId,
                FullName = model.FullName,
                Description = model.Description,
                SentDate = model.SentDate
            }).ToList();
            return View(testimonialVm);
        }
    }
}
