using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Areas.Admin.Models.AdminProjectVMs;
using MyPortfolio.Areas.Admin.Models.AdminSkillVMs;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Repositories.Concretes;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class SkillController : Controller
{
    private readonly ISkillRepository _skillRepository;

    public SkillController(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<IActionResult> Index()
    {
        var skills = await _skillRepository.GetAllSkillsAsync();
        var skillListVMs = skills?.Select(s => new AdminSkillListVM
        {
            SkillId = s.SkillId,
            Title = s.Title,
            SkillDescription = s.SkillDescription
        }).ToList() ?? new List<AdminSkillListVM>();

        return View(skillListVMs);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var skill = new AdminSkillCreateVM();
        return View(skill);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AdminSkillCreateVM adminSkillCreateVM)
    {
        if (!ModelState.IsValid)
        {
            return View(adminSkillCreateVM);
        }
        var adminSkillCreateVm = new Skill
        {
            SkillId = new int(),
            Title = adminSkillCreateVM.Title,
            SkillDescription = adminSkillCreateVM.SkillDescription
        };
        await _skillRepository.AddSkillAsync(adminSkillCreateVm);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var deletingSkill = await _skillRepository.DeleteSkillAsync(id);
        if (deletingSkill == null && deletingSkill.SkillId != id)
        {
            return View("Index");
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var skill = await _skillRepository.GetSkillByIdAsync(id);
        if (skill == null)
        {
            return RedirectToAction("Index");
        }
        // Veriler set edilerek Yeni bir AdminSkillUpdateVM nesnesi oluşturuluyor
        var skillVm = new AdminSkillUpdateVM
        {
            SkillId = skill.SkillId,
            Title = skill.Title,
            SkillDescription = skill.SkillDescription,
        };
        return View(skillVm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AdminSkillUpdateVM adminSkillUpdateVM)
    {
        if (!ModelState.IsValid)
        {
            return View(adminSkillUpdateVM);
        }

        var skill = await _skillRepository.GetSkillByIdAsync(adminSkillUpdateVM.SkillId);
        if (skill == null)
        {
            return RedirectToAction("Index"); // veya NotFound()
        }

        // Skill nesnesini güncelleme:
        skill.Title = adminSkillUpdateVM.Title;
        skill.SkillDescription = adminSkillUpdateVM.SkillDescription;

        await _skillRepository.UpdateSkillAsync(skill);

        return RedirectToAction("Index");
    }

}
