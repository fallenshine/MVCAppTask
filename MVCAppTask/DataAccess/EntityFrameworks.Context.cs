﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MunicipalityDBEntities : DbContext
    {
        public MunicipalityDBEntities()
            : base("name=MunicipalityDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LandProperty> LandProperties { get; set; }
        public virtual DbSet<Mortgage> Mortgages { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
    }
}