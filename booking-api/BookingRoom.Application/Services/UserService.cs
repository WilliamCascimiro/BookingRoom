using AutoMapper;
using BookingRoom.Application.DTOs.Auth;
using BookingRoom.Application.Features.Common;
using BookingRoom.Domain.Interfaces;
using System.Net;

namespace BookingRoom.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usuarioRepository;
        protected readonly IMapper _mapper;

        public UserService(IUserRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserLoginDTOOutput>> GetUser(UserLoginDTOInput userLoginDTOInput)
        {
            if(userLoginDTOInput.Email == null || userLoginDTOInput.Password == null)
                return Result<UserLoginDTOOutput>.Failure(HttpStatusCode.BadRequest, "Sessão expirada.");

            var user = await _usuarioRepository.GetUser(userLoginDTOInput.Email, userLoginDTOInput.Password);

            if (user is null)
                return Result<UserLoginDTOOutput>.Failure(HttpStatusCode.Unauthorized, "Usuário ou senha inválidos");

            var userOutput = _mapper.Map<UserLoginDTOOutput>(user);

            return Result<UserLoginDTOOutput>.Success(userOutput);
        }
    }
}
