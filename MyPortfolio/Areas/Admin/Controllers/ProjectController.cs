using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Areas.Admin.Models.AdminProjectVMs;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectRepository.GetAllProjectsAsync();
        var projectVm = projects.Select(model => new AdminProjectListVM
        {
            ProjectId = model.ProjectId,
            Title = model.Title,
            SubTitle = model.SubTitle,
            ProjectDescription = model.ProjectDescription,
            ProjectUrl = model.ProjectUrl
        }).ToList();
        return View(projectVm);

    }

    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectRepository.DeleteProject(id);
        if (project == null)
        {
            return View("Index");
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var project = new AdminProjectCreateVM();
        return View(project);

    }
    [HttpPost]
    public async Task<IActionResult> Create(AdminProjectCreateVM adminProjectCreateVM)
    {
        if (!ModelState.IsValid)
        {
            return View(adminProjectCreateVM);
        }
        var projectModel = new Project
        {
            Title = adminProjectCreateVM.Title,
            SubTitle = adminProjectCreateVM.SubTitle,
            ProjectDescription = adminProjectCreateVM.ProjectDescription,
            ProjectUrl = adminProjectCreateVM.ProjectUrl
        };
        await _projectRepository.CreateProject(projectModel);
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null)
        {
            return RedirectToAction("Index");
        }
        var projectVm = new AdminProjectUpdateVM
        {
            ProjectId = project.ProjectId,
            Title = project.Title,
            SubTitle = project.SubTitle,
            ProjectDescription = project.ProjectDescription,
            ProjectUrl = project.ProjectUrl
        };
        return View(projectVm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AdminProjectUpdateVM adminProjectUpdateVM)
    {
        if (!ModelState.IsValid)
        {
            return View(adminProjectUpdateVM);
        }
        var project = await _projectRepository.GetByIdAsync(adminProjectUpdateVM.ProjectId);
        if (project == null)
        {
            return RedirectToAction("Index");
        }
        project.Title = adminProjectUpdateVM.Title;
        project.SubTitle = adminProjectUpdateVM.SubTitle;
        project.ProjectDescription = adminProjectUpdateVM.ProjectDescription;
        project.ProjectUrl = adminProjectUpdateVM.ProjectUrl;

        await _projectRepository.UpdateAsync(project);
        return RedirectToAction("Index");

    }
}
