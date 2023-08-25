﻿using Microsoft.EntityFrameworkCore;
using PublisherDomain;
using System.Runtime.InteropServices.ComTypes;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(
             "Server=FRUIT; Database=PubDb; User Id=sa; Password=fru1t; Encrypt=False; TrustServerCertificate=True"
              );
            //optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDb");
        }

        //seed db
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName="Rhoda", LastName="Lerman"});

            var authroList = new Author[]
            {
                new Author { Id = 2,FirstName="Ruth", LastName="Ozeki"},
                new Author { Id = 3,FirstName="Sofia", LastName="Segovia"},
                new Author { Id = 4,FirstName="Ursula K.", LastName="Leguin"},
                new Author { Id = 5,FirstName="Hugh", LastName="Howey"},
                new Author { Id = 6,FirstName="Isabelle", LastName="Allende"},
                new Author { Id = 7,FirstName="Arthur", LastName="Waheire"},
            };
            modelBuilder.Entity<Author>().HasData( authroList );
        }
    }
}
