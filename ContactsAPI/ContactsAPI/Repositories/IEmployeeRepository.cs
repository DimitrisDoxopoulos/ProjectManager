
using ContactsAPI.DTO;
using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public interface IEmployeeRepository
    {
        Task<bool> InsertEmployeeAsync(EmployeeDTO request);
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> GetEmployeeByEmailAsync(string email);
        Task<Employee> UpdateEmployeeAsync(int id, EmployeeUpdateDTO request);
        Task<bool> DeleteEmployee(int id);
        Task<ICollection<Employee>> GetAllEmployeesAsync();
        Task<ICollection<Employee>> GetAllEmployeesOfUserAsync(int userId);
    }
}
