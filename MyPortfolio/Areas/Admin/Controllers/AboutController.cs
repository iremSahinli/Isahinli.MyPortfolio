using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Areas.Admin.Models.AdminAboutVMs;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class AboutController : Controller
{
    private readonly IAboutRepository _aboutRepository;

    public AboutController(IAboutRepository aboutRepository)
    {
        _aboutRepository = aboutRepository;
    }

    public async Task<IActionResult> Index()
    {
        var abouts = await _aboutRepository.GetAllAbouts();
        var aboutVm = abouts.Select(model => new AdminAboutListVM
        {
            AboutId = model.AboutId,
            Title = model.Title,
            SubDescription = model.SubDescription,
            MyPhoto = model.MyPhoto
        }).ToList();
        return View(aboutVm);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var aboutCreateVm = new AdminAboutCreateVM();
        return View(aboutCreateVm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AdminAboutCreateVM adminAboutCreateVM)
    {
        if (!ModelState.IsValid)
        {
            return View(adminAboutCreateVM);
        }

        byte[] photoBytes = null;
        if (adminAboutCreateVM.MyPhoto != null && adminAboutCreateVM.MyPhoto.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await adminAboutCreateVM.MyPhoto.CopyToAsync(memoryStream);
                photoBytes = memoryStream.ToArray();
            }
        }
        var aboutCreateVM = new About
        {

            Title = adminAboutCreateVM.Title,
            SubDescription = adminAboutCreateVM.SubDescription,
            MyPhoto = photoBytes
        };
        await _aboutRepository.AddAbout(aboutCreateVM);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var about = await _aboutRepository.GetById(id);
        var aboutVm = new AdminAboutUpdateVM
        {
            AboutId = about.AboutId,
            Title = about.Title,
            SubDescription = about.SubDescription,
            ExistingMyPhoto = about.MyPhoto != null ? $"data:image/*;base64,{Convert.ToBase64String(about.MyPhoto)}" : null,
        };
        return View(aboutVm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AdminAboutUpdateVM adminAboutUpdateVM)
    {
        if (!ModelState.IsValid)
        {
            return View(adminAboutUpdateVM);
        }
        var updatedAbout = await _aboutRepository.GetById(adminAboutUpdateVM.AboutId);
        if (updatedAbout == null)
        {
            return RedirectToAction("Index");
        }

        updatedAbout.Title = adminAboutUpdateVM.Title;
        updatedAbout.SubDescription = adminAboutUpdateVM.SubDescription;
        if (adminAboutUpdateVM.MyPhoto != null && adminAboutUpdateVM.MyPhoto.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await adminAboutUpdateVM.MyPhoto.CopyToAsync(memoryStream);
                updatedAbout.MyPhoto = memoryStream.ToArray();
            }
        }

        var updatingAbout = await _aboutRepository.UpdateAbout(updatedAbout);
        return RedirectToAction("Index");

    }

    public async Task<IActionResult> Delete(int id)
    {
        var deletingAbout = await _aboutRepository.DeleteAbout(id);
        if (deletingAbout == null)
        {
            return View("Index");
        }
        return RedirectToAction("Index");
    }
}
