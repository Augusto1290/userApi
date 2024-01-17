using AutoMapper;
using System.Net;
using System.Web.Http;
using UsersApi.Models.Post;
using UsersApi.Models.Post.Dto;
using UsersApi.Repositories;

namespace UsersApi.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepo;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<List<PostsDto>> GetAll()
        {
            var lista = await _postRepo.GetAll();
            return _mapper.Map<List<PostsDto>>(lista);
        }

        // Traer todos los posts de un usuario
        public async Task<List<PostsDto>> GetAllByUserId(int userId)
        {
            var lista = await _postRepo.GetAll(u => u.UserId == userId);
            return _mapper.Map<List<PostsDto>>(lista);
        }

        public async Task<PostDto> GetById(int id)
        {
            var post = await _postRepo.GetOne(u => u.Id == id);

            if (post == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> Create(CreatePostDto createPostDto)
        {
            var post = _mapper.Map<Post>(createPostDto);

            await _postRepo.Add(post);

            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> UpdateById(int id, UpdatePostDto updatePostDto)
        {
            var post = await _postRepo.GetOne(u => u.Id == id);

            if (post == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var updated = _mapper.Map(updatePostDto, post);

            return _mapper.Map<PostDto>(await _postRepo.Update(updated));
        }

        public async Task DeleteById(int id)
        {
            var post = await _postRepo.GetOne(u => u.Id == id);

            if (post == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            await _postRepo.Delete(post);
        }
    }
}