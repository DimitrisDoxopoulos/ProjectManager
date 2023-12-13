using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IEmployeesXProjectsService
    {
        Task AssignProjectAsync(params int[] request);
        Task<bool> DeleteAssignmentAsync(params int[] request);
        Task<IEnumerable<EmployeesXProject>> GetAllAssignmentsAsync();
    }
}
