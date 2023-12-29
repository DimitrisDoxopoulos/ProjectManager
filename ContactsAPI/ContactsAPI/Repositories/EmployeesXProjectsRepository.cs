using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Repositories
{
    public class EmployeesXProjectsRepository : BaseRepository<EmployeeProject>, IEmployeesXProjectsRepository
    {
        public EmployeesXProjectsRepository(ContactsAppContext context) : base(context) { }
        public async Task<bool> AssignProjectToEmployee(params int[] request)
        {
            var employeeId = request[0];
            var projectId = request[1];
            var userId = request[2];

            var employee = _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
            var project = _context.Projects.Where(x => x.Id == projectId).FirstOrDefault();
            if (employee is null || project is null) return false;
            var assignment = new EmployeeProject()
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
                UserId = userId
            };
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveEmployeeFromProject(params int[] request)
        {
            var employeeId = request[0];
            var projectId = request[1];
            var userId = request[2];

            var employee = await _context.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();
            var project = await _context.Projects.Where(x => x.Id == projectId).FirstOrDefaultAsync();
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (employee is null || project is null || user is null) return false;

            var assignment = await _context.EmployeeProjects.Where(
                x => x.EmployeeId == employee.Id && x.ProjectId == project.Id && x.UserId == user.Id
                ).FirstOrDefaultAsync();
            _context.EmployeeProjects.Remove(assignment!);
            return true;
        }

        public async Task<IEnumerable<EmployeeProject>> GetAllAssignmentsAsync()
        {
            List<EmployeeProject> assignments = await _context.EmployeeProjects.ToListAsync();
            return assignments;
        }

        public async Task<IEnumerable<EmployeeProject>> GetAllAssignmentsOfUserAsync(int userId)
        {
            List<EmployeeProject> assignments = await _context.EmployeeProjects.Where(x => x.UserId == userId).ToListAsync();
            return assignments;
        }
    }
}
