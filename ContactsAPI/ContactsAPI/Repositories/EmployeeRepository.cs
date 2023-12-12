using ContactsAPI.DTO;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
            var existingEmployee = await _context.Employees.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (existingEmployee is not null) return false;
            var employee = new Employee()
            {
                UserId = request.UserId,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email,
                CompanyRole = request.CompanyRole,
                Slug = generateSlug(request.Email!)
            };
            await _context.Employees.AddAsync(employee);
            return true;
        }

        public async Task<Employee> GetEmployeeBySlugAsync(string slug)
        {
            return await _context.Employees.Where(x => x.Slug == slug).FirstOrDefaultAsync();
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, EmployeeUpdateDTO request)
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

        private string generateSlug(string input)
        {
            string lowercased = input.ToLowerInvariant();
            StringBuilder slugBuilder = new StringBuilder();

            foreach (char c in lowercased)
            {
                if (char.IsLetterOrDigit(c) || c == '-')
                {
                    slugBuilder.Append(c);
                }
                else if (c == ' ')
                {
                    slugBuilder.Append('-');
                }
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(slugBuilder.ToString()));
                string hashedSlug = BitConverter.ToString(hashBytes, 0, 4).Replace("-", "").ToLowerInvariant();
                return hashedSlug;
            }
        }
    }
}
