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
    public static class TerminlaRequestFormatter
    {
        public static Terminal Convert(this TerminalDTO terminalDTO)
        {
            Terminal terminal = Mapper.Map<TerminalDTO, Terminal>(terminalDTO);
            return terminal;
        }
    }
}
