using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Resturant.Model;
using Resturant.Repository;
using AutoMapper;

namespace Resturant.Service.RequestFormatters
{
    public static class UserRequestFormatter
    {
        public static User Convert(this UserDTO userDTO)
        {
            User user = Mapper.Map<UserDTO, User>(userDTO);
            return user;
        }
    }
}
