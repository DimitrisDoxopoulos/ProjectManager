using ContactsAPI.DTO;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ContactsAPI.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ContactsAppContext context) : base(context) { }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees.Where(x => x.Email == email).FirstOrDefaultAsync();

        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<bool> InsertEmployeeAsync(EmployeeDTO request)
        {
            try
            {
                var existingEmployee = await _context.Employees.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
                if (existingEmployee is not null) return false;

                var employee = new Employee()
                {
                    UserId = request.UserId,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Email = request.Email,
                    CompanyRole = request.CompanyRole,
                };

                await _context.Employees.AddAsync(employee);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, EmployeeUpdateDTO request)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            employee.Email = request.Email;
            employee.Firstname = request.Firstname;
            employee.Lastname = request.Lastname;
            employee.CompanyRole = request.CompanyRole;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<ICollection<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Include(p => p.Projects)
                .ToArrayAsync();
        }

        public async Task<ICollection<Employee>> GetAllEmployeesOfUserAsync(int userId)
        {
            return await _context.Employees.Where(x => x.UserId == userId).Include(p => p.Projects).ToArrayAsync();
        }
    }
}
