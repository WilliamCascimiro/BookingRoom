//using BookingRoom.Application.DTOs.Booking;
//using BookingRoom.Application.Services;
//using BookingRoom.Domain.Entities;
//using BookingRoom.Domain.Interfaces;
//using Moq;
//using System.Linq.Expressions;

//namespace BookingRoom.Tests
//{
//    public class BookingServiceTests
//    {
//        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
//        private readonly Mock<IRoomTimeSlotRepository> _timeSlotRepositoryMock;
//        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
//        private readonly BookingService _bookingService;

//        public BookingServiceTests()
//        {
//            _bookingRepositoryMock = new Mock<IBookingRepository>();
//            _timeSlotRepositoryMock = new Mock<IRoomTimeSlotRepository>();
//            _unitOfWorkMock = new Mock<IUnitOfWork>();
//            _bookingService = new BookingService(
//                _bookingRepositoryMock.Object,
//                _timeSlotRepositoryMock.Object,
//                _unitOfWorkMock.Object);
//        }

//        [Fact]
//        public async Task Create_SimultaneousBooking_ShouldOnlyAllowOne()
//        {
//            // Arrange
//            var createBookingRequest = new CreateBookingRequest
//            {
//                userId = Guid.NewGuid(),
//                roomId = Guid.NewGuid(),
//                id = new List<Guid> { Guid.NewGuid() }
//            };

//            var timeSlot = new RoomTimeSlot
//            {
//                Id = createBookingRequest.id.First(),
//                IsBooked = false,
//                BookingId = null
//            };

//            var timeSlotList = new List<RoomTimeSlot>();
//            timeSlotList.Add(timeSlot);

//            _timeSlotRepositoryMock
//                .Setup(repo => repo.Buscar(It.IsAny<Expression<Func<RoomTimeSlot, bool>>>()))
//                .ReturnsAsync(new List<RoomTimeSlot> { timeSlot });

//            _timeSlotRepositoryMock
//                .Setup(repo => repo.Update(It.IsAny<RoomTimeSlot>()))
//                .Callback((RoomTimeSlot ts) =>
//                {
//                    ts.IsBooked = true;
//                    ts.BookingId = createBookingRequest.roomId;
//                });

//            // Simulate two parallel requests
//            var task1 = _bookingService.Create(createBookingRequest, timeSlotList);
//            var task2 = _bookingService.Create(createBookingRequest, timeSlotList);

//            // Act
//            var results = await Task.WhenAll(task1, task2);

//            // Assert
//            var successfulBookings = results.Count(result => result.IsSuccess);
//            var failedBookings = results.Count(result => !result.IsSuccess);

//            Assert.Equal(1, successfulBookings); // Only one should succeed
//            Assert.Equal(1, failedBookings);     // The other should fail
//        }


//        [Fact]
//        public async Task Update_SimultaneousBooking_ShouldOnlyAllowOne()
//        {
//            // Arrange
//            var bookingId = Guid.NewGuid();

//            var timeSlotToAdd = new RoomTimeSlot
//            {
//                Id = Guid.NewGuid(),
//                IsBooked = false,
//                BookingId = null
//            };

//            var timeSlotToRemove = new RoomTimeSlot
//            {
//                Id = Guid.NewGuid(),
//                IsBooked = true,
//                BookingId = bookingId
//            };

//            var booking = new Booking
//            {
//                Id = bookingId
//            };

//            _timeSlotRepositoryMock
//                .Setup(repo => repo.Update(It.IsAny<RoomTimeSlot>()))
//                .Callback((RoomTimeSlot ts) =>
//                {
//                    if (ts.Id == timeSlotToAdd.Id)
//                    {
//                        ts.IsBooked = true;
//                        ts.BookingId = bookingId;
//                    }
//                    else if (ts.Id == timeSlotToRemove.Id)
//                    {
//                        ts.IsBooked = false;
//                        ts.BookingId = null;
//                    }
//                });

//            //_unitOfWorkMock.Setup(uow => uow.BeginTransactionAsync()).Returns(Task.CompletedTask);
//            //_unitOfWorkMock.Setup(uow => uow.SaveChangesAndCommitAsync()).Returns(Task.CompletedTask);
//            //_unitOfWorkMock.Setup(uow => uow.RollbackAsync()).Returns(Task.CompletedTask);

//            // Simulate two parallel update requests
//            var task1 = _bookingService.Update(new[] { timeSlotToAdd }, new[] { timeSlotToRemove }, booking);
//            var task2 = _bookingService.Update(new[] { timeSlotToAdd }, new[] { timeSlotToRemove }, booking);

//            // Act
//            var results = await Task.WhenAll(task1, task2);

//            // Assert
//            var successfulUpdates = results.Count(result => result.IsSuccess);
//            var failedUpdates = results.Count(result => !result.IsSuccess);

//            Assert.Equal(1, successfulUpdates); // Only one update should succeed
//            Assert.Equal(1, failedUpdates);     // The other should fail
//        }

//    }
//}
