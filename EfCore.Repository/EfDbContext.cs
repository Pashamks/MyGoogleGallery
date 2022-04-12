using MyGooglegallery.EfCore.Repository.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace MyGooglegallery.EfCore.Repository
{
    public class EfDbContext : DbContext
    {
        protected string _connectionString = "Data Source=DESKTOP-SM098C1;Initial Catalog=MyGoogleGalleryDb;Integrated Security=True";
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public EfDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(_connectionString);
            base.OnConfiguring(builder);
        }
    }
}
