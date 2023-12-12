namespace ContactsAPI.Services
{
    public interface IEmployeesXProjectsService
    {
        Task AssignProjectAsync(int employeeId, int projectId);
        Task<bool> DeleteAssignmentAsync(int employeeId, int projectId);
    }
}
