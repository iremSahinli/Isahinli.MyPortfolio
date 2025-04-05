using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Repositories.Interfaces;

public interface ISkillRepository
{
    //Sahip olunan teknolojileri listeler.
    Task<IEnumerable<Skill>> GetAllSkillsAsync();
    Task<Skill> AddSkillAsync(Skill skill);
    Task<Skill> UpdateSkillAsync(Skill skill);
    Task<Skill> DeleteSkillAsync(int skillId);
    Task<Skill> GetSkillByIdAsync(int skillId);
}
