using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IProjectService
    {
        Task InsertProjectAsync(ProjectDTO request);
        Task<Project> UpdateProjectAsync(ProjectUpdateDTO request, int id);
        bool DeleteProject(int id);
        Task<Project> GetProject(int id);
        Task<Project> GetProjectBySlugAsync(string slug);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<IEnumerable<Project>> GetAllProjectsOfUserAsync(int userId);
    }
}
