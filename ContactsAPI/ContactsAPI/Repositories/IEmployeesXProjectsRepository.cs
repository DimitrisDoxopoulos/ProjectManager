using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public interface IEmployeesXProjectsRepository
    {
        Task<bool> AssignProjectToEmployee(params int[] request);
        Task<bool> RemoveEmployeeFromProject(params int[] request);
        Task<IEnumerable<EmployeeProject>> GetAllAssignmentsOfUserAsync(int userId);
        Task<IEnumerable<EmployeeProject>> GetAllAssignmentsAsync();
    }
}
