using ContactsAPI.DTO;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ContactsAppContext context) : base(context) { }

        public bool DeleteProjectAsync(int id)
        {
            var project = _context.Projects.Where(x => x.Id == id).FirstOrDefault();
            if (project == null) return false;
            _context.Projects.Remove(project);
            return true;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            List<Project> projects = await _context.Projects.ToListAsync();
            return projects;
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            return await _context.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertProjectAsync(ProjectDTO request)
        {
            var project = new Project()
            {
                UserId = request.UserId,
                EmployeeId = request.EmployeeId,
                Description = request.Description,
                Deadline = request.Deadline
            };

            await _context.Projects.AddAsync(project);
            return true;
        }

        public async Task<Project> UpdateProjectAsync(int id, ProjectDTO request)
        {
            var project = await _context.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (project is null) return null;
            project.UserId = request.UserId;
            project.EmployeeId = request.EmployeeId;
            project.Description = request.Description;
            project.Deadline = request.Deadline;

            _context.Projects.Update(project);
            return project;
        }
    }
}
