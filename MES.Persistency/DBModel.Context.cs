﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MES.Persistency
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBModelContainer : DbContext
    {
        public DBModelContainer()
            : base("name=DBModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<DBDataIdentifier> DBDataIdentifiers { get; set; }
        public DbSet<DBDataItem> DBDataItems { get; set; }
        public DbSet<DBDataParameter> DBDataParameters { get; set; }
        public DbSet<ProductKeyIDSerialNumberPairs> ProductKeyIDSerialNumberPairs { get; set; }
    }
}
