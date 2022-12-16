using ReservationSystem2022.Models;

namespace ReservationSystem2022.Services
{
    public interface IUserService
    {
        public Task<UserDTO> CreateUserAsync(User user);


    }
}
