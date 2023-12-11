using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContactsAppContext _context;
        public IUserRepository UserRepository => new UserRepository(_context);
        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_context);
        public IProjectRepository ProjectRepository => new ProjectRepository(_context);

        public UnitOfWork(ContactsAppContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
