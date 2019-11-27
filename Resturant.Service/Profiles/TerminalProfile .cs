
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Resturant.Model;
using Resturant.Repository;

namespace Resturant.Service.Profiles
{
    public class TerminalProfile: Profile
    {
        public TerminalProfile()
        {
            CreateMap<Terminal, TerminalDTO>();
        }
    }
}
