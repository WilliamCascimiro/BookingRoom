using AutoMapper;
using BookingRoom.Application.DTOs.Booking;
using BookingRoom.Application.Features.Common;
using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookingRoom.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomTimeSlotRepository _timeSlotRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public BookingService(IBookingRepository bookingRepository, IRoomTimeSlotRepository timeSlotRepository, IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _timeSlotRepository = timeSlotRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<BookingDTOOutput>> Create(CreateBookingRequestOld createBookingRequest)
        {
            try
            {
                if (createBookingRequest == null || createBookingRequest?.id?.Count() <= 0)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Reserve ao menos uma data/hora.");

                if (createBookingRequest?.userId == null || createBookingRequest?.roomId == null)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Algo deu errado na solicitação.");

                var timeSlotList = await _timeSlotRepository.Buscar(t => createBookingRequest.id.Contains(t.Id));

                var isConsistent = await CheckConsistency(timeSlotList, createBookingRequest);
                if (!isConsistent)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Algo deu errado na solicitação");

                var isAvailable = await CheckAvailability(createBookingRequest.id);
                if (!isAvailable)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Sala já reservada para essa data e horário");

                var result = await CreateTransactional(createBookingRequest, timeSlotList);

                if (!result.IsSuccess)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, result.Error);

                return Result<BookingDTOOutput>.Success(null);
            }
            catch (Exception)
            {
                return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
        }

        public async Task<Result<BookingDTOOutput>> CreateTransactional(CreateBookingRequestOld createBookingRequest, IEnumerable<TimeSlot> timeSlotList)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var newBooking = new Booking(createBookingRequest.userId, createBookingRequest.roomId);
                await _bookingRepository.Adcionar(newBooking);

                foreach (var timeSlot in timeSlotList)
                {
                    timeSlot.IsBooked = true;
                    timeSlot.BookingId = newBooking.Id;
                    await _timeSlotRepository.Update(timeSlot);
                }

                await _unitOfWork.SaveChangesAndCommitAsync();
                return Result<BookingDTOOutput>.Success(null);
            }
            catch (DbUpdateException)
            {
                await _unitOfWork.RollbackAsync();
                return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
        }

        public async Task<Result<BookingDTOOutput>> Update(UpdateBookingRequest updateBookingRequest)
        {
            try
            {
                if (updateBookingRequest == null || updateBookingRequest?.timeSlotsId?.Count() <= 0)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Reserve ao menos uma data/hora.");

                if (updateBookingRequest?.userId == null || updateBookingRequest?.roomId == null)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Algo deu errado na solicitação.");

                var booking = await _bookingRepository.GetById(updateBookingRequest.bookingId);
                var oldTimeSlotList = await _timeSlotRepository.GetTimeSlotsByBookingId(updateBookingRequest.bookingId);
                var newTimeSlotList = await _timeSlotRepository.Buscar(t => updateBookingRequest.timeSlotsId.Contains(t.Id));

                var addedTimeSlots = newTimeSlotList.Where(x => !oldTimeSlotList.Select(x => x.Id).Contains(x.Id)).ToList();
                var removedTimeSlots = oldTimeSlotList.Where(x => !newTimeSlotList.Select(x => x.Id).Contains(x.Id)).ToList();

                if (!ValidateSequentialTimes(newTimeSlotList.ToList()))
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Os horários selecionados devem estar em sequência.");

                var isAvailable = await CheckAvailability(addedTimeSlots.Select(x => x.Id).ToList());
                if (!isAvailable)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Sala já reservada para essa data e horário");

                var result = await UpdateTransactional(addedTimeSlots, removedTimeSlots, booking);

                if (!result.IsSuccess)
                    return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, result.Error);

                return Result<BookingDTOOutput>.Success(null);
            }
            catch (Exception)
            {
                return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
        }

        public async Task<Result<BookingDTOOutput>> UpdateTransactional(IEnumerable<TimeSlot> addedTimeSlots, IEnumerable<TimeSlot> removedTimeSlots, Booking booking)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (removedTimeSlots?.Count() >= 1)
                {
                    foreach (var timeSlot in removedTimeSlots)
                    {
                        timeSlot.IsBooked = false;
                        timeSlot.BookingId = null;
                        await _timeSlotRepository.Update(timeSlot);
                    }
                }

                if (addedTimeSlots?.Count() >= 1)
                {
                    foreach (var timeSlot in addedTimeSlots)
                    {
                        timeSlot.IsBooked = true;
                        timeSlot.BookingId = booking.Id;
                        await _timeSlotRepository.Update(timeSlot);
                    }
                }

                await _unitOfWork.SaveChangesAndCommitAsync();
                return Result<BookingDTOOutput>.Success(null);
            }
            catch (DbUpdateException)
            {
                await _unitOfWork.RollbackAsync();
                return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Sala já reservada para essa data e horário");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return Result<BookingDTOOutput>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
        }

        public async Task<Result<bool>> Delete(string bookingId)
        {
            try
            {
                if (bookingId == null)
                    return Result<bool>.Failure(HttpStatusCode.BadRequest, "Selecione uma reserva para consultar.");

                var booking = await _bookingRepository.GetById(new Guid(bookingId));
                var timeSlotList = await _timeSlotRepository.GetTimeSlotsByBookingId(new Guid(bookingId));

                await _unitOfWork.BeginTransactionAsync();

                foreach (var timeSlot in timeSlotList)
                {
                    timeSlot.IsBooked = false;
                    timeSlot.BookingId = null;
                    timeSlot.Booking = null;
                    await _timeSlotRepository.Update(timeSlot);
                }

                await _bookingRepository.Remover(booking.Id);

                await _unitOfWork.SaveChangesAndCommitAsync();


                return Result<bool>.Success(true); ;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result<bool>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
        }

        public async Task<Result<IEnumerable<ListBookingResponse>>> ListAllBookingsByUser(string userId)
        {
            try
            {
                if (userId == null)
                    return Result<IEnumerable<ListBookingResponse>> .Failure(HttpStatusCode.BadRequest, "Selecione uma reserva para consultar.");

                var bookings = await _bookingRepository.GetAllByUser(new Guid(userId));

                if (bookings == null || bookings.Count() <= 0)
                    return Result<IEnumerable<ListBookingResponse>>.Failure(HttpStatusCode.BadRequest, "Reserva não encontrada.");

                var listBookingResponse = new List<ListBookingResponse>();
                foreach (var booking in bookings)
                {
                    TimeOnly? dataInicial = booking.RoomTimeSlots?.Select(x => x.Time).Min();
                    TimeOnly? dataFinal = booking.RoomTimeSlots?.Select(x => x.Time).Max();

                    var bookingResponse = new ListBookingResponse();
                    bookingResponse.Id = booking.Id;
                    bookingResponse.UserId = booking.UserId;
                    bookingResponse.RoomId = booking.RoomId;
                    bookingResponse.RoomName = booking.Room.Name;
                    bookingResponse.ResponsibleUser = booking.User.Name;
                    bookingResponse.Date = booking.RoomTimeSlots?.Select(x => x.Date).FirstOrDefault().ToString() ?? "Data não disponível";
                    bookingResponse.HoraInicial = dataInicial.ToString() ?? "Hora final não disponível";
                    bookingResponse.HoraFinal = dataFinal?.AddHours(1).ToString() ?? "Hora final não disponível";
                    listBookingResponse.Add(bookingResponse);
                }

                listBookingResponse = listBookingResponse.OrderBy(x => DateTime.Parse(x.Date))
                    .ThenBy(x => x.HoraInicial).ToList();

                return Result<IEnumerable<ListBookingResponse>>.Success(listBookingResponse);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ListBookingResponse>>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
        }

        public async Task<Result<IEnumerable<ListBookingResponse>>> ListBookingsFromAllUsers()
        {
            try
            {
                var bookings = await _bookingRepository.GetFromAllUsers();

                if (bookings == null || bookings.Count() <= 0)
                    return Result<IEnumerable<ListBookingResponse>>.Failure(HttpStatusCode.BadRequest, "Nenhuma reserva encontrada.");

                var listBookingResponse = new List<ListBookingResponse>();
                foreach (var booking in bookings)
                {
                    TimeOnly? dataInicial = booking.RoomTimeSlots?.Select(x => x.Time).Min();
                    TimeOnly? dataFinal = booking.RoomTimeSlots?.Select(x => x.Time).Max();

                    var bookingResponse = new ListBookingResponse();
                    bookingResponse.Id = booking.Id;
                    bookingResponse.UserId = booking.UserId;
                    bookingResponse.RoomId = booking.RoomId;
                    bookingResponse.RoomName = booking.Room.Name;
                    bookingResponse.ResponsibleUser = booking.User.Name;
                    bookingResponse.Date = booking.RoomTimeSlots?.Select(x => x.Date).FirstOrDefault().ToString() ?? "Data não disponível";
                    bookingResponse.HoraInicial = dataInicial.ToString() ?? "Hora final não disponível";
                    bookingResponse.HoraFinal = dataFinal?.AddHours(1).ToString() ?? "Hora final não disponível";
                    listBookingResponse.Add(bookingResponse);
                }

                listBookingResponse = listBookingResponse.OrderBy(x => DateTime.Parse(x.Date))
                    .ThenBy(x => x.HoraInicial).ToList();

                return Result<IEnumerable<ListBookingResponse>>.Success(listBookingResponse);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ListBookingResponse>>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
        }

        public async Task<IEnumerable<Booking>> ListAllBookings()
        {
            var bookings = await _bookingRepository.GetAll();

            return bookings;
        }

        public async Task<Result<DetailBookingResponse>> GetBookingById(string bookingId)
        {
            try
            {
                if (bookingId == null)
                    return Result<DetailBookingResponse>.Failure(HttpStatusCode.BadRequest, "Selecione uma reserva para consultar.");

                var booking = await _bookingRepository.GetById(new Guid(bookingId));
                var timeSlotList = await _timeSlotRepository.GetTimeSlotsByBookingId(new Guid(bookingId));

                if (booking == null)
                    return Result<DetailBookingResponse>.Failure(HttpStatusCode.BadRequest, "Reserva não encontrada.");

                var detailBookingResponse = new DetailBookingResponse();
                detailBookingResponse.Date = timeSlotList[0].Date;
                detailBookingResponse.roomId = booking.RoomId;
                detailBookingResponse.timeSlotSelected.AddRange(timeSlotList.Select(x => x.Id));

                return Result<DetailBookingResponse>.Success(detailBookingResponse);
            }
            catch (Exception ex)
            {
                return Result<DetailBookingResponse>.Failure(HttpStatusCode.BadRequest, "Ocorreu um erro");
            }
        }

        private async Task<bool> CheckAvailability(List<Guid> timeSlotId)
        {
            var timeSlot = await _timeSlotRepository.Buscar(t => timeSlotId.Contains(t.Id));

            if(timeSlot.Any(x => x.IsBooked))
                return false;
                
            return true;
        }

        private async Task<bool> CheckConsistency(IEnumerable<TimeSlot>? timeSlotsFromDataBase, CreateBookingRequestOld request)
        {
            //Verifica se a lista está vazia
            if (timeSlotsFromDataBase == null || timeSlotsFromDataBase?.Count() <= 0)
                return false;

            //Verifica se todos os ids vindo da request foram encontrados no banco de dados
            if (timeSlotsFromDataBase?.Count() != request.id.Count())
                return false;

            //Verifica se todos os agendamentos são para a mesma data
            var firstDate = timeSlotsFromDataBase.First().Date;
            if(!timeSlotsFromDataBase.All(r => r.Date == firstDate))
                return false;

            //Verifica se o id da sala da request e do banco de dados são iguais
            if (!timeSlotsFromDataBase.All(r => r.RoomId == request.roomId))
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
    }
}
