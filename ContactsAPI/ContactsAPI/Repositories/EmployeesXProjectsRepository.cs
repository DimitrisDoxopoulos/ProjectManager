using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Repositories
{
    public class EmployeesXProjectsRepository : BaseRepository<EmployeesXProject>, IEmployeesXProjectsRepository
    {
        public EmployeesXProjectsRepository(ContactsAppContext context) : base(context) { }
        public async Task<bool> AssignProjectToEmployee(params int[] request)
        {
            var employeeId = request[0];
            var projectId = request[1];

            var employee = _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
            var project = _context.Projects.Where(x => x.Id == projectId).FirstOrDefault();
            if (employee is null || project is null) return false;
            var assignment = new EmployeesXProject()
            {
                EmployeeId = employeeId,
                ProjectId = projectId
            };
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveEmployeeFromProject(params int[] request)
        {
            var employeeId = request[0];
            var projectId = request[1];

            var employee = _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
            var project = _context.Projects.Where(x => x.Id == projectId).FirstOrDefault();
            if (employee is null || project is null) return false;

            var assignment = _context.EmployeesXProjects.Where(
                x => x.EmployeeId == employeeId && x.ProjectId == projectId
                ).FirstOrDefault();
            _context.EmployeesXProjects.Remove(assignment);
            return true;
        }

        public async Task<IEnumerable<EmployeesXProject>> GetAllAssignmentsAsync()
        {
            List<EmployeesXProject> assignments = await _context.EmployeesXProjects.ToListAsync();
            return assignments;
        }
    }
}
