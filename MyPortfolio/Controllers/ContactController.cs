using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Contect;
using MyPortfolio.DAL.Entities;
using System.Text.RegularExpressions;

namespace MyPortfolio.Controllers;

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
                TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi, onaylandıktan sonra sayfada görünecek.";
                return RedirectToAction("Index", "Portfolio");
            }
            return View("Error");
        }
        catch (Exception ex)
        {

            return View("Index", "Portfolio");
        }
    }

    [HttpPost]
    public JsonResult ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Json(false);
        }
        var allowedDomains = new List<string>
        {
            "gmail.com",
            "hotmail.com",
            "yahoo.com",
            "windowslive.com",
            "icloud.com",
        };

        var allowedExtensions = new List<string>
        {
            "com", "net", "org", "edu", "gov"
        };
        try
        {
            var emailParts = email.Split('@');
            if (emailParts.Length != 2)
            {
                return Json(false);
            }

            var domain = emailParts[1].ToLower();

            // domain kontrolü
            var domainParts = domain.Split('.');
            if (domainParts.Length < 2)
            {
                return Json(false);
            }

            var extension = domainParts.Last();

            bool isAllowedDomain = allowedDomains.Contains(domain);
            bool isAllowedExtension = allowedExtensions.Contains(extension);

            //Email format kontrolü: 
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            bool isValidFormat = emailRegex.IsMatch(email);

            return Json(isValidFormat && isAllowedDomain && isAllowedExtension);
        }
        catch (Exception)
        {

            return Json(false);
        }
    }
}
