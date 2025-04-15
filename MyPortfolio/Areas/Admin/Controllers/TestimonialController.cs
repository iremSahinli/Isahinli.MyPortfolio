using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Areas.Admin.Models;
using MyPortfolio.Areas.Admin.Models.AdminTestimonialVM;
using MyPortfolio.DAL.Contect;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class TestimonialController : Controller
{
    private readonly ITestimonialRepository _testimonialRepository;
    private readonly AppDbContext _context;

    public TestimonialController(ITestimonialRepository testimonialRepository, AppDbContext context)
    {
        _testimonialRepository = testimonialRepository;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var testimonials = await _testimonialRepository.GetAllTestimonialsAsync();
        var testimonialVm = testimonials.Select(model => new AdminTestimonialVM
        {
            TestimonialId = model.TestimonialId,
            FullName = model.FullName,
            Description = model.Description,
            SentDate = model.SentDate,
            IsRead = model.IsRead ? true : false,
            IsApproved = model.IsApproved
        }).ToList();
        return View(testimonialVm);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var testimonial = await _testimonialRepository.DeleteTestimonial(id);
        if (testimonial == null)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var testimonial = await _testimonialRepository.GetTestimonialId(id);
        if (testimonial == null)
        {
            return View();
        }

        if (!testimonial.IsRead)
        {
            testimonial.IsRead = true;
            _context.Update(testimonial);
            await _context.SaveChangesAsync();
        }
        var testimonialVm = new AdminTestimonialDetailVM
        {
            TestimonialId = testimonial.TestimonialId,
            FullName = testimonial.FullName,
            Description = testimonial.Description,
            SentDate = testimonial.SentDate,
            IsRead = true,
            IsApproved = testimonial.IsApproved
        };
        return View(testimonialVm);
    }

    [HttpPost]
    public async Task<IActionResult> ToogleApproval(int id, bool isApproved)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(t => t.TestimonialId == id);
        if (testimonial == null)
        {
            return NotFound();
        }
        testimonial.IsApproved = isApproved;
        await _context.SaveChangesAsync();
        return Ok();
    }
}
