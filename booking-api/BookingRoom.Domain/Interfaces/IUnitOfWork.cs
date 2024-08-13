namespace BookingRoom.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        void Dispose();
        Task RollbackAsync();
        Task<int> SaveChangesAndCommitAsync();
        Task<int> SaveChangesAsync();
    }
}
