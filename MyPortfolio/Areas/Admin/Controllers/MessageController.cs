using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Areas.Admin.Models;
using MyPortfolio.DAL.Contect;

namespace MyPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Messages()
        {
            var messages = await _context.Contacts
                         .OrderByDescending(m => m.SentDate)
                                        .Select(m => new AdminMessageVM
                                        {
                                            ContactId = m.ContactId,
                                            FirstName = m.FirstName,
                                            Email = m.Email,
                                            Message = m.Message,
                                            SentDate = m.SentDate,
                                            IsMessageRead = m.IsMessageRead,

                                        }).ToListAsync();

            return View(messages);
        }
    }
}
