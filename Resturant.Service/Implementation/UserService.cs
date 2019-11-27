using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.Model;
using Resturant.Repository;
using Resturant.Repository.Implementation;
using Resturant.Repository.Interface;
using Resturant.Service.ResponseFormatters;
using Resturant.Service.RequestFormatters;

namespace Resturant.Service.Implementation
{

    #region Interface
    public interface IUserService
    {
        int CreateUser(UserDTO userDTO);
        List<UserDTO> GetAllUsers();
        UserDTO GetUserById(int id);
        bool UpdateUser(UserDTO userDTO);
        bool DeleteUser(int userId);
        UserDTO ValidateUser(UserDTO userDTO);
    }
    #endregion

    #region Implementation
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public List<UserDTO> GetAllUsers()
        {
            List<User> users = _unitOfWork.UserRepository.All().ToList();
            return users.Convert();
        }

        public int CreateUser(UserDTO userDTO)
        {
            User user = userDTO.Convert();
            _unitOfWork.UserRepository.Insert(user);
            return _unitOfWork.Save();
        }

        public UserDTO GetUserById(int id)
        {
            User user = _unitOfWork.UserRepository.GetById(id);
            UserDTO userDTO = user.Convert();
            return userDTO;
        }

        public Boolean UpdateUser(UserDTO userDTO)
        {
            User _editingUser = _unitOfWork.UserRepository.GetById(userDTO.UserId);
            _editingUser.Email = userDTO.Email;
            _editingUser.Password = userDTO.Password;

            //_editingTerminal = terminalDTO.Convert();
            _unitOfWork.UserRepository.Update(_editingUser);
            return true;

        }

        public bool DeleteUser(int userId)
        {
            _unitOfWork.UserRepository.Delete(userId);
            return true;
        }

        public UserDTO ValidateUser(UserDTO userDTO)
        {
            User user = _unitOfWork.UserRepository.Filter(x => x.Email == userDTO.Email && x.Password == userDTO.Password).FirstOrDefault();
            return user.Convert();
        }

    }

    #endregion
}
