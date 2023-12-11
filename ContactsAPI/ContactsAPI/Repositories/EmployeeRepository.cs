using ContactsAPI.DTO;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ContactsAppContext context) : base(context){}

        public bool DeleteEmployee(int id)
        {
            var employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee == null) return false;
            _context.Employees.Remove(employee);
            return true;
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees.Where(x => x.Email == email).FirstOrDefaultAsync();
  
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertEmployeeAsync(EmployeeDTO request)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingEmployee != null) return false;
            var employee = new Employee()
            {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email,
                CompanyRole = request.CompanyRole
            };
            _context.Employees.AddAsync(employee);
            return true;
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, EmployeeDTO request)
        {
            var employee = await _context.Employees.Where(x => x.Id == id).FirstAsync();
            employee.Email = request.Email;
            employee.Firstname = request.Firstname;
            employee.Lastname = request.Lastname;
            employee.CompanyRole = request.CompanyRole;

            _context.Employees.Update(employee);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> Employees = await _context.Employees.ToListAsync();
            return Employees;
        }
    }
}
