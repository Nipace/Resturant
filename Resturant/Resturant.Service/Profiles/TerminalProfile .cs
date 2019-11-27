
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using TMDoyle.Model;
using TMDoyle.Repository;

namespace TMDoyle.Service.Profiles
{
    public class TerminalProfile: Profile
    {
        public TerminalProfile()
        {
            CreateMap<Terminal, TerminalDTO>();
        }
    }
}
