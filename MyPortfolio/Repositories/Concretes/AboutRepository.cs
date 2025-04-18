using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Contect;
using MyPortfolio.DAL.Entities;
using MyPortfolio.Repositories.Interfaces;

namespace MyPortfolio.Repositories.Concretes;

public class AboutRepository : IAboutRepository
{
    private readonly AppDbContext _context;

    public AboutRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<About>> GetAllAbouts()
    {
        var abouts = await _context.Abouts.ToListAsync();
        return abouts;
    }

    public async Task<About> AddAbout(About about)
    {
        try
        {
            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();
            return about;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<About> UpdateAbout(About about)
    {
        try
        {
            _context.Abouts.Update(about);
            await _context.SaveChangesAsync();
            return about;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<About> DeleteAbout(int id)
    {
        try
        {
            var deletingAbout = await _context.Abouts.FirstOrDefaultAsync(x => x.AboutId == id);
            if (deletingAbout == null)
            {
                throw new Exception("About not found");
            }
            _context.Abouts.Remove(deletingAbout);
            await _context.SaveChangesAsync();
            return deletingAbout;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<About> GetById(int id)
    {
        try
        {
            if (_context.Abouts == null && _context.Abouts.Any())
            {
                throw new Exception("About not found");
            }
            return await _context.Abouts.FirstOrDefaultAsync(x => x.AboutId == id);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

}
