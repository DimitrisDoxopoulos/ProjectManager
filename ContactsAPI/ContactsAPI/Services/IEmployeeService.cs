using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IEmployeeService
    {
        Task InsertEmployeeAsync(EmployeeDTO request);
        Task<Employee> UpdateEmployeeAsync(EmployeeUpdateDTO request, int id);
        bool DeleteEmployee(int id);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> GetEmployeeByEmailAsync(string email);
        Task<Employee> GetEmployeeBySlugAsync(string slug);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}
