using AutoMapper;
using BookingRoom.Application.DTOs.Auth;
using BookingRoom.Application.DTOs.Booking;
using BookingRoom.Domain.Entities;

namespace Mottu.Application.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, UserLoginDTOInput>().ReverseMap();
            CreateMap<User, UserLoginDTOOutput>().ReverseMap();
            CreateMap<Booking, BookingDTOInput>().ReverseMap();
            CreateMap<Booking, UserLoginDTOOutput>().ReverseMap();
        }
    }
}
