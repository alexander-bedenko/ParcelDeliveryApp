using System;
using System.Linq;
using AutoMapper;
using ParcelDelivery.BLL.DTO;
using ParcelDelivery.BLL.Modules;
using ParcelDelivery.DAL.Entities;
using ParcelDelivery.DAL.UoW;
using Serilog;

namespace ParcelDelivery.BLL.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserDTO AutheticateUser(string login, string password)
        {
            var user = _unitOfWork.Repository<User>().Find(x => x.Login == login).FirstOrDefault();
            while (true)
            {
                if (IsUserValid(user, password))
                    break;
                return null;
            }

            return Mapper.Map<UserDTO>(user);
        }

        public UserDTO FindUser(string login)
        {
            Log.Information("FindUser");
            var user = _unitOfWork.Repository<User>().Find(u => u.Login == login).FirstOrDefault();
            return Mapper.Map<User, UserDTO>(user);
        }

        public void RegisterUser(UserDTO userDto)
        {
            try
            {
                userDto.Password = HashProvider.Hash(userDto.Password);
                var regUser = Mapper.Map<User>(userDto);
                _unitOfWork.Repository<User>().Create(regUser);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception thrown when trying to register new user", e);
            }
        }

        public void EditUser(UserDTO userDto)
        {
            try
            {
                var user = _unitOfWork.Repository<User>().Find(u => u.Login == userDto.Login).FirstOrDefault();

                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Password = HashProvider.Hash(userDto.Password);
                user.Email = userDto.Email;

                _unitOfWork.Repository<User>().Update(user);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception thrown when trying to edit user", e);
            }
        }

        public void DeleteUser(UserDTO userDto)
        {
            try
            {
                var user = _unitOfWork.Repository<User>().Find(u => u.Login == userDto.Login).FirstOrDefault();

                _unitOfWork.Repository<User>().Delete(user?.Id);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception thrown when trying to delete user", e);
            }
        }

        private bool IsUserValid(User user, string password)
        {
            var hashedPassword = HashProvider.Hash(password);
            return user?.Password == hashedPassword;
        }
    }
}
