using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Model
{
    public class TerminalDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public short TBTRNFileNumber { get; set; }
        public int CompanyId { get; set; }
        public int CreatedById { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public System.DateTime ModifiedOn { get; set; }

        
    }
}
