//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMA.Logics.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TerminalServer
    {
        public int Id { get; set; }
        public string TsName { get; set; }
        public bool Status { get; set; }
        public int IdServer { get; set; }
    
        public virtual Server Server { get; set; }
    }
}
