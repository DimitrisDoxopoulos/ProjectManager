using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IEmployeesXProjectsService
    {
        Task AssignProjectAsync(params int[] request);
        Task<bool> DeleteAssignmentAsync(params int[] request);
        Task<IEnumerable<EmployeeProject>> GetAllAssignmentsOfUserAsync(int userId);
        Task<IEnumerable<EmployeeProject>> GetAllAssignmentsAsync();
    }
}
