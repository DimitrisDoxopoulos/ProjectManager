using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IProjectService
    {
        Task InsertProjectAsync(ProjectDTO request);
        Task<Project> UpdateProjectAsync(ProjectDTO request, int id);
        bool DeleteProject(int id);
        Task<Project> GetProject(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
    }
}
