using AutoMapper;
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

        public async Task AssignProjectAsync(int employeeId, int projectId)
        {
            await _unitOfWork.SaveAsync(); ;
        }

        public async Task<bool> DeleteAssignmentAsync(int employeeId, int projectId)
        {
            _unitOfWork.EmployeesXProjectsRepository.RemoveEmployeeFromProject(employeeId, projectId);
            return true;
        }
    }
}
