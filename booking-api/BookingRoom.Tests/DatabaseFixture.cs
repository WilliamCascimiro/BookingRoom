﻿using BookingRoom.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public BookingDbContext DbContext { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<BookingDbContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookingRoom;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options;

            DbContext = new BookingDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
