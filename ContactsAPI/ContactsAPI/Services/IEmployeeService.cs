using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IEmployeeService
    {
        Task InsertEmployeeAsync(EmployeeDTO request);
        Task<Employee> UpdateEmployeeAsync(EmployeeDTO request, int id);
        bool DeleteEmployee(int id);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> GetEmployeeByEmailAsync(string email);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}
