namespace ContactsAPI.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IProjectRepository ProjectRepository { get; }

       Task<bool> SaveAsync();
    }
}
