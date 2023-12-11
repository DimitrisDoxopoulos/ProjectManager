using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public interface IProjectRepository
    {
        Task<bool> InsertProjectAsync(ProjectDTO request);
        Task<Project> UpdateProjectAsync(int id, ProjectDTO request);
        Task<Project> GetProjectAsync(int id);
        bool DeleteProjectAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();

    }
}
