using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using TMDoyle.Service.Profiles;

namespace TMDoyle.Service
{
    //class AutoMapperConfiguration
    //{
    //}

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<TerminalProfile>();
                config.AddProfile<UserProfile>();
            });
        }
    }
}
