using CarpoolManagement.Persistance.Models;
using CarpoolManagement.Source.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarpoolManagement.Persistance
{
    public class CarpoolContext : DbContext
    {
        public virtual DbSet<EmployeeEntity> Employee { get; set; }
        public virtual DbSet<CarEntity> Car { get; set; }
        public virtual DbSet<RideShareEntity> RideShare { get; set; }
        public virtual DbSet<RideShareEmployeeEntity> RideShareEmployee { get; set; }

        public CarpoolContext(DbContextOptions<CarpoolContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // using System.Diagnostics;
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarpoolContext).Assembly, type => type.Namespace != null && type.Namespace.EndsWith(".Persistance.Models"));

            modelBuilder.Entity<CarEntity>().HasData(
                    new CarEntity() { Id = 1, Plate = "AB 123-CD", Name = "Blue Beetle - Commute Transport", Type = "VW Beetle", Color = Color.Blue, NumberOfSeats = 4 },
                    new CarEntity() { Id = 2, Plate = "CD 456-EF", Name = "Mustang - Quick support", Type = "Ford Mustang", Color = Color.Gray, NumberOfSeats = 4 },
                    new CarEntity() { Id = 3, Plate = "EF 789-GH", Name = "Octavia - Travel", Type = "Skoda Octavia", Color = Color.Black, NumberOfSeats = 5 },
                    new CarEntity() { Id = 4, Plate = "GH 123-IJ", Name = "Carnival - Team Travel", Type = "Kia Carnival", Color = Color.Red, NumberOfSeats = 7 },
                    new CarEntity() { Id = 5, Plate = "IJ 456-KL", Name = "Tacoma - Off Road Travel", Type = "Toyota Tacoma", Color = Color.Green, NumberOfSeats = 4 },
                    new CarEntity() { Id = 6, Plate = "KL 789-MN", Name = "Fabia #1 - Basic Travel", Type = "Skoda Fabia", Color = Color.White, NumberOfSeats = 5 },
                    new CarEntity() { Id = 7, Plate = "MN 123-OP", Name = "Fabia #2 - Basic Travel", Type = "Skoda Fabia", Color = Color.White, NumberOfSeats = 5 },
                    new CarEntity() { Id = 8, Plate = "OP 465-QR", Name = "Fabia #3 - Basic Travel", Type = "Skoda Fabia", Color = Color.White, NumberOfSeats = 5 },
                    new CarEntity() { Id = 9, Plate = "QR 789-ST", Name = "Camaro - Quick support", Type = "Chevrolet Camaro", Color = Color.Yellow, NumberOfSeats = 4 },
                    new CarEntity() { Id = 10, Plate = "ST 123-UV", Name = "Bus - Interurban transport", Type = "Iveco Crossway", Color = Color.Other, NumberOfSeats = 63 }
                );

            modelBuilder.Entity<EmployeeEntity>().HasData(
                    new EmployeeEntity { Id = 1, Name = "Sebastiana Chaudhari", IsDriver = true },
                    new EmployeeEntity { Id = 2, Name = "Garbán De Santiago", IsDriver = true },
                    new EmployeeEntity { Id = 3, Name = "Verginia McCallum", IsDriver = true },
                    new EmployeeEntity { Id = 4, Name = "Joleen Storstrand", IsDriver = true },
                    new EmployeeEntity { Id = 5, Name = "Durga Robbins", IsDriver = true },
                    new EmployeeEntity { Id = 6, Name = "Aeliana Grant", IsDriver = false },
                    new EmployeeEntity { Id = 7, Name = "Hamo Kumar", IsDriver = false },
                    new EmployeeEntity { Id = 8, Name = "Oskar Arnaud", IsDriver = false },
                    new EmployeeEntity { Id = 9, Name = "Rolando Waller", IsDriver = false },
                    new EmployeeEntity { Id = 10, Name = "Adam Dreessen", IsDriver = false },
                    new EmployeeEntity { Id = 11, Name = "Elyse Ray", IsDriver = true },
                    new EmployeeEntity { Id = 12, Name = "Arlo Tyler", IsDriver = true },
                    new EmployeeEntity { Id = 13, Name = "Helena Houston", IsDriver = true },
                    new EmployeeEntity { Id = 14, Name = "Sylas Vo", IsDriver = true },
                    new EmployeeEntity { Id = 15, Name = "Artemis Maxwell", IsDriver = true },
                    new EmployeeEntity { Id = 16, Name = "Eden Leblanc", IsDriver = false },
                    new EmployeeEntity { Id = 17, Name = "Novalee Dennis", IsDriver = false },
                    new EmployeeEntity { Id = 18, Name = "Joanna Mendez", IsDriver = false },
                    new EmployeeEntity { Id = 19, Name = "Arthur Poole", IsDriver = false },
                    new EmployeeEntity { Id = 20, Name = "Bonnie Flynn", IsDriver = false },
                    new EmployeeEntity { Id = 21, Name = "Kannon Boyer", IsDriver = true },
                    new EmployeeEntity { Id = 22, Name = "Chaya Ashley", IsDriver = true },
                    new EmployeeEntity { Id = 23, Name = "Kylen Woods", IsDriver = true },
                    new EmployeeEntity { Id = 24, Name = "Reese Dougherty", IsDriver = true },
                    new EmployeeEntity { Id = 25, Name = "Brett Sierra", IsDriver = true },
                    new EmployeeEntity { Id = 26, Name = "Kohen Hill", IsDriver = false },
                    new EmployeeEntity { Id = 27, Name = "Lillie Becker", IsDriver = false },
                    new EmployeeEntity { Id = 28, Name = "Andy Mata", IsDriver = false },
                    new EmployeeEntity { Id = 29, Name = "Zev Alvarez", IsDriver = false },
                    new EmployeeEntity { Id = 30, Name = "Raylan Lane", IsDriver = false }
                );
        }
    }
}
