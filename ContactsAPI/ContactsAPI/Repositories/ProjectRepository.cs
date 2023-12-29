using ContactsAPI.DTO;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ContactsAPI.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ContactsAppContext context) : base(context) { }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            try
            {
                var project = await _context.Projects.FindAsync(id);
                if (project == null)
                {
                    return false;
                }

                _context.Projects.Remove(project);
                int affectedRows = await _context.SaveChangesAsync();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ICollection<Project>> GetAllProjectsAsync()
        {
            ICollection<Project> projects = await _context.Projects.Include(e => e.Employees).ToArrayAsync();
            return projects;
        }

        public async Task<ICollection<Project>> GetAllProjectsOfUserAsync(int userId)
        {
            ICollection<Project> projects = await _context.Projects.Where(x => x.UserId == userId).Include(e => e.Employees).ToArrayAsync();
            return projects;
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            return await _context.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertProjectAsync(ProjectDTO request)
        {
            try
            {
                var project = new Project()
                {
                    Title = request.Title,
                    UserId = request.UserId,
                    Description = request.Description,
                    Deadline = request.Deadline,
                };

                await _context.Projects.AddAsync(project);
                int affectedRows = await _context.SaveChangesAsync();

                return affectedRows > 0; // Return true if the project was successfully inserted
            }
            catch (DbUpdateException)
            {
                // Handle database update errors if necessary
                return false;
            }
        }

        public async Task<Project?> UpdateProjectAsync(int id, ProjectUpdateDTO request)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project is null) return null;

            project.Title = request.Title;
            project.UserId = request.UserId;
            project.Description = request.Description;
            project.Deadline = request.Deadline;

            try
            {
                _context.Projects.Update(project);
                int affectedRows = await _context.SaveChangesAsync();

                return affectedRows > 0 ? project : null;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}
