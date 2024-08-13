using BookingRoom.Application.DTOs.Auth;
using BookingRoom.Application.Features.Common;
using BookingRoom.Domain.Interfaces;

namespace BookingRoom.Application.Services
{
    public interface IUserService
    {
        Task<Result<UserLoginDTOOutput>> GetUser(UserLoginDTOInput userLogin);
    }
}
