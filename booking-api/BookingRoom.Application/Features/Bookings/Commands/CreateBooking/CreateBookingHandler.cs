using BookingRoom.Application.Features.Common;
using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;
using MediatR;
using System.Net;

namespace BookingRoom.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingRequest, Result<CreateBookingResponse>>
    {
        IBookingRepository _repository;
        IRoomTimeSlotRepository _timeSlotRepository;

        public CreateBookingHandler(IBookingRepository repository, IRoomTimeSlotRepository timeSlotRepository)
        {
            _repository = repository;
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<Result<CreateBookingResponse>> Handle(CreateBookingRequest createBookingRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (!new CreateBookingRequestValidator().Validate(createBookingRequest).IsValid)
                    return Result<CreateBookingResponse>.Failure(HttpStatusCode.BadRequest, "Reserve ao menos uma data/hora.");

                var timeSlotList = await _timeSlotRepository.Buscar(timeSlot => createBookingRequest.timeSlotsId.Contains(timeSlot.Id.ToString()));

                var isConsistent = await CheckConsistency(timeSlotList, createBookingRequest);
                if (!isConsistent)
                    return Result<CreateBookingResponse>.Failure(HttpStatusCode.BadRequest, "Algo deu errado na solicitação");

                var isAvailable = await CheckAvailability(createBookingRequest.timeSlotsId);
                if (!isAvailable)
                    return Result<CreateBookingResponse>.Failure(HttpStatusCode.BadRequest, "Sala já reservada para essa data e horário");

                //var result = await CreateTransactional(createBookingRequest, timeSlotList);

                //if (!result.IsSuccess)
                //    return Result<CreateBookingResponse>.Failure(HttpStatusCode.BadRequest, result.Error);

                return Result<CreateBookingResponse>.Success(null);
            }
            catch (Exception)
            {
                return Result<CreateBookingResponse>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }

        }

        //public async Task<Result<BookingDTOOutput>> CreateTransactional(CreateBookingRequest createBookingRequest, IEnumerable<TimeSlot> timeSlotList)
        //{
        //    try
        //    {
        //        //await _unitOfWork.BeginTransactionAsync();

        //        var newBooking = new Booking(createBookingRequest.userId, createBookingRequest.roomId);
        //        await _bookingRepository.Adcionar(newBooking);

        //        foreach (var timeSlot in timeSlotList)
        //        {
        //            timeSlot.IsBooked = true;
        //            timeSlot.BookingId = newBooking.Id;
        //            await _timeSlotRepository.Update(timeSlot);
        //        }

        //        await _unitOfWork.SaveChangesAndCommitAsync();
        //        return Result<BookingDTOOutput>.Success(null);
        //    }
        //    catch (DbUpdateException)
        //    {
        //        await _unitOfWork.RollbackAsync();
        //        return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
        //    }
        //    catch (Exception)
        //    {
        //        await _unitOfWork.RollbackAsync();
        //        return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
        //    }
        //}

        private async Task<bool> CheckConsistency(IEnumerable<TimeSlot>? timeSlotsFromDataBase, CreateBookingRequest request)
        {
            //Verifica se a lista está vazia
            if (timeSlotsFromDataBase == null || timeSlotsFromDataBase?.Count() <= 0)
                return false;

            //Verifica se todos os ids vindo da request foram encontrados no banco de dados
            if (timeSlotsFromDataBase?.Count() != request.timeSlotsId.Count())
                return false;

            //Verifica se todos os agendamentos são para a mesma data
            var firstDate = timeSlotsFromDataBase.First().Date;
            if (!timeSlotsFromDataBase.All(r => r.Date == firstDate))
                return false;

            //Verifica se o id da sala da request e do banco de dados são iguais
            if (!timeSlotsFromDataBase.All(r => r.RoomId.ToString() == request.roomId))
                return false;

            return true;
        }

        private bool ValidateSequentialTimes(List<TimeSlot> timeSlots)
        {
            // Ordena a lista por Time
            var sortedTimeSlots = timeSlots.OrderBy(ts => ts.Time).ToList();

            // Verifica a sequência
            for (int i = 1; i < sortedTimeSlots.Count; i++)
            {
                var previousTimeSlot = sortedTimeSlots[i - 1];
                var currentTimeSlot = sortedTimeSlots[i];

                // Verifica se o Time do próximo TimeSlot é o próximo na sequência
                if (currentTimeSlot.Time != previousTimeSlot.Time.AddHours(1))
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> CheckAvailability(List<string> timeSlotId)
        {
            var timeSlot = await _timeSlotRepository.Buscar(t => timeSlotId.Contains(t.Id.ToString()));

            if (timeSlot.Any(x => x.IsBooked))
                return false;

            return true;
        }

    }
}
