namespace ContactsAPI.Repositories
{
    public interface IEmployeesXProjectsRepository
    {
        public Task<bool> AssignProjectToEmployee(int employeeId, int projectId);
        public Task<bool> RemoveEmployeeFromProject(int employeeId, int projectId);
    }
}
