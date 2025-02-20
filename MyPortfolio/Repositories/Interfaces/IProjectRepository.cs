using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetByIdAsync(int projectId);
        Task<Project> DeleteProject(int projectId);
        Task<Project> CreateProject(Project project);
        Task UpdateAsync(Project project);
    }
}
