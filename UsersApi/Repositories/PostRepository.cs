using UsersApi.Models.Post;
using UsersApi.Services;

namespace UsersApi.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> Update(Post entity);
    }

    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _db;

        public PostRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Post> Update(Post entity)
        {
            _db.Posts.Update(entity);
            await Save();
            return entity;
        }
    }
}
