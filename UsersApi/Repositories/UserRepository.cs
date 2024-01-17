using UsersApi.Models.User;
using UsersApi.Services;

namespace UsersApi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Update(User entity);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<User> Update(User entity)
        {
            _db.Users.Update(entity);
            await Save();
            return entity;
        }
    }
}

