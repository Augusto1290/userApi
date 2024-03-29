﻿using AutoMapper;
using System.Net;
using System.Web.Http;
using UsersApi.Models.User;
using UsersApi.Models.User.Dto;
using UsersApi.Repositories;

namespace UsersApi.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly PostService _postService;
        private readonly IEncoderService _encoderService;

        public UserService(IUserRepository userRepo, IMapper mapper, PostService postService, IEncoderService encoderService)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _postService = postService;
            _encoderService = encoderService;
        }

        public async Task<List<UsersDto>> GetAll()
        {
            var lista = await _userRepo.GetAll();
            return _mapper.Map<List<UsersDto>>(lista);
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _userRepo.GetOne(u => u.Id == id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var posts = await _postService.GetAllByUserId(id);

            var mapped = _mapper.Map<UserDto>(user);

            mapped.Posts = posts;

            return mapped;
        }

        public async Task<UserDto> Create(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            user.Password = _encoderService.Encode(user.Password);

            await _userRepo.Add(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateById(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepo.GetOne(u => u.Id == id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var updated = _mapper.Map(updateUserDto, user);

            return _mapper.Map<UserDto>(await _userRepo.Update(updated));
        }

        public async Task DeleteById(int id)
        {
            var user = await _userRepo.GetOne(u => u.Id == id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            await _userRepo.Delete(user);
        }
    }
}