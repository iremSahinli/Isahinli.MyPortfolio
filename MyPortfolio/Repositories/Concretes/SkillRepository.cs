using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Contect;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.Repositories.Concretes;

public class SkillRepository : ISkillRepository
{
    private readonly AppDbContext _context;

    public SkillRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
    {
        var skills = _context.Skills.ToListAsync();
        if (skills == null)
        {
            return await Task.FromResult<IEnumerable<Skill>>(new List<Skill>());
        }
        return await skills;
    }

    public async Task<Skill> AddSkillAsync(Skill skill)
    {
        try
        {
            _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return skill;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Skill> DeleteSkillAsync(int skillId)
    {
        try
        {
            var skill = await _context.Skills.FirstOrDefaultAsync(p => p.SkillId == skillId);
            if (skill != null)
            {
                _context.Remove(skill);
                await _context.SaveChangesAsync();
            }
            return skill;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<Skill> GetSkillByIdAsync(int skillId)
    {
        try
        {
            var skill = await _context.Skills.FindAsync(skillId);
            return skill;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<Skill> UpdateSkillAsync(Skill skill)
    {
        try
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            return skill;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
