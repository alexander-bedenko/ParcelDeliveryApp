using ParcelDelivery.BLL.DTO;

namespace ParcelDelivery.BLL.Services
{
    /// <summary>
    /// CRUD operations with user
    /// </summary>
    public interface IUserService
    {
        UserDTO AutheticateUser(string login, string password);
        UserDTO FindUser(string login);
        void RegisterUser(UserDTO userDto);
        void EditUser(UserDTO userDto);
        void DeleteUser(UserDTO userDto);
    }
}