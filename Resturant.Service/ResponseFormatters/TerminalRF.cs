using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Resturant.Repository;
using Resturant.Model;
using AutoMapper;

namespace Resturant.Service.ResponseFormatters
{
    public static class TerminalRF
    {
        public static List<TerminalDTO> Convert(this List<Terminal> terminals)
        {
            List<TerminalDTO> terminalsDTO = Mapper.Map<List<Terminal>, List<TerminalDTO>>(terminals);
            return terminalsDTO;

        }

        public static TerminalDTO Convert(this Terminal terminal)
        {
            return Mapper.Map<Terminal, TerminalDTO>(terminal);
        }

    }
}
