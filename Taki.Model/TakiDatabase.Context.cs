﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Taki.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class takiEntities : DbContext
    {
        public takiEntities()
            : base("name=takiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<role> role { get; set; }
        public DbSet<rolemenu> rolemenu { get; set; }
        public DbSet<sysmenu> sysmenu { get; set; }
        public DbSet<user> user { get; set; }
        public DbSet<userrole> userrole { get; set; }
    }
}
