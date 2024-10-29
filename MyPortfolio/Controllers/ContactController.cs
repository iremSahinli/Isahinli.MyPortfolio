using Microsoft.AspNetCore.Mvc;
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
