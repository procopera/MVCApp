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
    
    public partial class Database
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Version { get; set; }
        public string Schema { get; set; }
        public bool status { get; set; }
        public int IdServer { get; set; }
    
        public virtual Server Server { get; set; }
    }
}
