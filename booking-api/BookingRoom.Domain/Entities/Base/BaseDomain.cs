namespace BookingRoom.Domain.Entities
{
    public abstract class BaseDomain
    {
        public Guid Id { get; set; } = new Guid();
    }
}
