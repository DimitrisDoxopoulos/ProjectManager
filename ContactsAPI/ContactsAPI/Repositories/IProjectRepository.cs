using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public interface IProjectRepository
    {
        Task<bool> InsertProjectAsync(ProjectDTO request);
        Task<Project> UpdateProjectAsync(int id, ProjectUpdateDTO request);
        Task<Project> GetProjectAsync(int id);
        Task<Project> GetProjectBySlugAsync(string slug);
        bool DeleteProjectAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<IEnumerable<Project>> GetAllProjectsOfUserAsync(int userId);
    }
}
