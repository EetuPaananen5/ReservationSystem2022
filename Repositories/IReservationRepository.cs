using ReservationSystem2022.Models;

namespace ReservationSystem2022.Repositories
{
    public interface IReservationRepository
    {
        public Task<Reservation> GetReservationAsync(long id);
        public Task<IEnumerable<Reservation>> GetReservationsAsync();
        public Task<IEnumerable<Reservation>> GetReservationsAsync(User user);
        public Task<IEnumerable<Reservation>> GetReservationsAsync(Item target, DateTime start, DateTime end);
        public Task<Reservation> AddReservationAsync( Reservation reservation);
        public Task<Reservation> UpdateReservationAsync(Reservation reservation);
        public Task<Boolean> DeleteReservationAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetReservationAsync();
    }
}
