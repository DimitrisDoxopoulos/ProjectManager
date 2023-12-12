namespace ContactsAPI.Services
{
    public interface IApplicationService
    {
        IUserService UserService { get; }
        IProjectService ProjectService { get; }
        IEmployeeService EmployeeService { get; }
        IEmployeesXProjectsService EmployeesXProjectsService { get; }
    }
}
