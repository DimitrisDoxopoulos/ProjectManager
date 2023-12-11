using AutoMapper;
using ContactsAPI.DTO;
using ContactsAPI.Models;
using ContactsAPI.Repositories;

namespace ContactsAPI.Services
{
    public class EmployeesService : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool DeleteEmployee(int id)
        {
            var contact = _unitOfWork.EmployeeRepository.GetEmployeeById(id);
            if (contact is null) throw new ApplicationException("Contact Does not exist");
            _unitOfWork.EmployeeRepository.DeleteEmployee(id);
            return true;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = (List<Employee>)await _unitOfWork.EmployeeRepository.GetAllEmployeesAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByEmailAsync(email);
            if (employee is null) throw new ApplicationException("Employee with email " +  email + " does not exist");
            return employee;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeById(id);
            if (employee is null) throw new ApplicationException("Employee with email " + id + " does not exist");
            return employee;
        }

        public async Task InsertEmployeeAsync(EmployeeDTO request)
        {
            if (await _unitOfWork.EmployeeRepository.GetEmployeeByEmailAsync(request.Email!) is null)
                throw new ApplicationException("Employee already exists");
            await _unitOfWork.SaveAsync();
        }

        public async Task<Employee> UpdateEmployeeAsync(EmployeeDTO request, int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.UpdateEmployeeAsync(id, request);
            await _unitOfWork.SaveAsync();
            return employee;
        }
    }
}
