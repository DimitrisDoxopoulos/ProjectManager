using ContactsAPI.Repositories;
using AutoMapper;

namespace ContactsAPI.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IUserService UserService => new UserService(_unitOfWork, _mapper);
        public IProjectService ProjectService => new ProjectService(_unitOfWork, _mapper);
        public IEmployeeService EmployeeService => new EmployeesService(_unitOfWork, _mapper);
        public IEmployeesXProjectsService EmployeesXProjectsService => new EmployeesXProjectsService(_unitOfWork, _mapper);
    }
}
