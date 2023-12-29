using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IProjectService
    {
        Task InsertProjectAsync(ProjectDTO request);
        Task<Project> UpdateProjectAsync(ProjectUpdateDTO request, int id);
        Task<bool> DeleteProjectAsync(int id);
        Task<Project> GetProjectAsync(int id);
        Task<ICollection<Project>> GetAllProjectsAsync();
        Task<ICollection<Project>> GetAllProjectsOfUserAsync(int userId);
    }
}
