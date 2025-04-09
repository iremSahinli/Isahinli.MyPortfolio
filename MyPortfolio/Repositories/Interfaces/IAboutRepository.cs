using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Repositories.Interfaces;

public interface IAboutRepository
{
    Task<IEnumerable<About>> GetAllAbouts();
    Task<About> AddAbout(About about);
    Task<About> UpdateAbout(About about);
    Task<About> DeleteAbout(int id);
    Task<About> GetById(int id);
}
