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

        public bool DeleteProject(int id)
        {
            var project = _unitOfWork.ProjectRepository.GetProjectAsync(id);
            if (project == null) throw new ApplicationException("Project does not exist");
            _unitOfWork.ProjectRepository.DeleteProjectAsync(id);
            return true;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            List<Project> projects = (List<Project>)await _unitOfWork.ProjectRepository.GetAllProjectsAsync();
            return projects;
        }

        public async Task<Project> GetProject(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectAsync(id);
            if (project is null) throw new ApplicationException("Project does not exist");
            return project;
        }

        public async Task InsertProjectAsync(ProjectDTO request)
        {
            await _unitOfWork.ProjectRepository.InsertProjectAsync(request);
        }

        public async Task<Project> UpdateProjectAsync(ProjectDTO request, int id)
        {
            var project = await _unitOfWork.ProjectRepository.UpdateProjectAsync(id, request);
            await _unitOfWork.SaveAsync();
            return project;
        }
    }
}
