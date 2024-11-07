using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Contect;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SendMessage(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contact.SentDate = DateTime.Now;
                    contact.IsMessageRead = false;
                    await _context.Contacts.AddAsync(contact);

                    var testimonial = new Testimonial
                    {
                        FullName = contact.FirstName,
                        SentDate = contact.SentDate,
                        Description = contact.Message,
                    };
                    await _context.Testimonials.AddAsync(testimonial);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Portfolio");
                }
                return View("Error");
            }
            catch (Exception ex)
            {

                return View("Index", "Portfolio");
            }







        }
    }
}
