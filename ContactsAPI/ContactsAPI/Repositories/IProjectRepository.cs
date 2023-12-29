using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public interface IProjectRepository
    {
        Task<bool> InsertProjectAsync(ProjectDTO request);
        Task<Project> UpdateProjectAsync(int id, ProjectUpdateDTO request);
        Task<Project> GetProjectAsync(int id);
        Task<bool> DeleteProjectAsync(int id);
        Task<ICollection<Project>> GetAllProjectsAsync();
        Task<ICollection<Project>> GetAllProjectsOfUserAsync(int userId);
    }
}
