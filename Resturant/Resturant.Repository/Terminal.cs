//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resturant.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Terminal
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