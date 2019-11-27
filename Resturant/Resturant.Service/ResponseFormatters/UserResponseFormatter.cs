using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMDoyle.Repository;
using TMDoyle.Model;
using AutoMapper;

namespace TMDoyle.Service.ResponseFormatters
{
    public static class UserResponseFormatter
    {
        public static List<UserDTO> Convert(this List<User> users)
        {
            List<UserDTO> usersDTO = Mapper.Map<List<User>, List<UserDTO>>(users);
            return usersDTO;

        }

        public static UserDTO Convert(this User user)
        {
            return Mapper.Map<User, UserDTO>(user);
        }

    }
}
