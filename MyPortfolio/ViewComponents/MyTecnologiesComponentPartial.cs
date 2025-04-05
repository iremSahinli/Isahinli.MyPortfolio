using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Areas.Admin.Models.AdminSkillVMs;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.ViewComponents
{
    public class MyTecnologiesComponentPartial : ViewComponent
    {
        private readonly ISkillRepository _skillRepository;

        public MyTecnologiesComponentPartial(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var skills = await _skillRepository.GetAllSkillsAsync();
            var skillVms = skills?.Select(s => new AdminSkillListVM
            {
                SkillId = s.SkillId,
                Title = s.Title,
                SkillDescription = s.SkillDescription

            }).ToList() ?? new List<AdminSkillListVM>();
            return View(skillVms);
        }
    }
}
