using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RoleBasedAuthorization.Models
{
    public partial class MyDbContext : DbContext
    {
        //public MyDbContext()
        //{
        //}

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<LinkRolesMenus> LinkRolesMenus { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Task> Task { get; set; }


    }
}
