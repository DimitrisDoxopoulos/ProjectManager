using AutoMapper;
using ContactsAPI.Models;
using ContactsAPI.Repositories;

namespace ContactsAPI.Services
{
    public class EmployeesXProjectsService : IEmployeesXProjectsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesXProjectsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AssignProjectAsync(params int[] request)
        {
            await _unitOfWork.SaveAsync(); ;
        }

        public async Task<bool> DeleteAssignmentAsync(params int[] request)
        {
            await _unitOfWork.EmployeesXProjectsRepository.RemoveEmployeeFromProject(request);
            return true;
        }

        public async Task<IEnumerable<EmployeeProject>> GetAllAssignmentsAsync()
        {
            List<EmployeeProject> assignments = (List<EmployeeProject>) await _unitOfWork.EmployeesXProjectsRepository.GetAllAssignmentsAsync();
            return assignments;
        }

        public async Task<IEnumerable<EmployeeProject>> GetAllAssignmentsOfUserAsync(int userId)
        {
            List<EmployeeProject> assignments = (List<EmployeeProject>) await _unitOfWork.EmployeesXProjectsRepository.GetAllAssignmentsOfUserAsync(userId);
            return assignments;
        }
    }
}
