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

        public async Task<IEnumerable<Project>> GetAllProjectsOfUserAsync(int userId)
        {
            List<Project> projects = await _context.Projects.Where(x => x.UserId == userId).ToListAsync();
            return projects;
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            return await _context.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Project> GetProjectBySlugAsync(string slug)
        {
            return await _context.Projects.Where(x => x.Slug == slug).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertProjectAsync(ProjectDTO request)
        {
            var project = new Project()
            {
                Title = request.Title,
                UserId = request.UserId,
                Description = request.Description,
                Deadline = request.Deadline,
                Slug = generateSlug(request.Title)
            };
            await _context.Projects.AddAsync(project);
            return true;
        }

        public async Task<Project?> UpdateProjectAsync(int id, ProjectUpdateDTO request)
        {
            var project = await _context.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (project is null) return null;

            project.Title = request.Title;
            project.UserId = request.UserId;
            project.Description = request.Description;
            project.Deadline = request.Deadline;
            project.Slug = generateSlug(request.Title);

            _context.Projects.Update(project);
            return project;
        }

        public string generateSlug(string input)
        {
            string lowercased = input.ToLowerInvariant();
            StringBuilder slugBuilder = new StringBuilder();

            foreach (char c in lowercased)
            {
                if (char.IsLetterOrDigit(c) || c == '-')
                {
                    slugBuilder.Append(c);
                } else if (c == ' ')
                {
                    slugBuilder.Append('-');
                }
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(slugBuilder.ToString()));
                string hashedSlug = BitConverter.ToString(hashBytes, 0, 4).Replace("-", "").ToLowerInvariant();
                return hashedSlug;
            }
        }
    }
}
