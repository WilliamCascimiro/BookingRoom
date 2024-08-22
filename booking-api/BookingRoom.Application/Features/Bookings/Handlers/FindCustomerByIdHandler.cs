using BookingRoom.Application.Features.Bookings.Queries.Requests;
using BookingRoom.Application.Features.Bookings.Queries.Responses;
using BookingRoom.Domain.Interfaces;

namespace BookingRoom.Application.Features.Bookings.Handlers
{
    public class FindCustomerByIdHandler : IFindCustomerByIdHandler
    {
        IBookingRepository _repository;

        public FindCustomerByIdHandler(IBookingRepository repository)
        {
            _repository = repository;
        }
        public FindCustomerByIdResponse Handle(FindCustomerByIdRequest command)
        {
            // TODO: Lógica de leitura se houver

            // Retorna o resultado
            //return _repository.GetCustomerById(command.Id);
            return new FindCustomerByIdResponse();
        }
    }

}
