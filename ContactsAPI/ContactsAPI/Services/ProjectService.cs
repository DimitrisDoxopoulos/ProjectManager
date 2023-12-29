using AutoMapper;
using ContactsAPI.DTO;
using ContactsAPI.Models;
using ContactsAPI.Repositories;

namespace ContactsAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectAsync(id);
            if (project == null) throw new ApplicationException("Project does not exist");
            _unitOfWork.ProjectRepository.DeleteProjectAsync(id);
            return true;
        }

        public async Task<ICollection<Project>> GetAllProjectsAsync()
        {
            ICollection<Project> projects = await _unitOfWork.ProjectRepository.GetAllProjectsAsync();
            return projects;
        }

        public async Task<ICollection<Project>> GetAllProjectsOfUserAsync(int userId)
        {
            ICollection<Project> projects = await _unitOfWork.ProjectRepository.GetAllProjectsOfUserAsync(userId);
            return projects;
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectAsync(id);
            if (project is null) throw new ApplicationException("Project does not exist");
            return project;
        }

        public async Task InsertProjectAsync(ProjectDTO request)
        {
            await _unitOfWork.SaveAsync();
        }

        public async Task<Project> UpdateProjectAsync(ProjectUpdateDTO request, int id)
        {
            var project = await _unitOfWork.ProjectRepository.UpdateProjectAsync(id, request);
            await _unitOfWork.SaveAsync();
            return project;
        }

    }
}
