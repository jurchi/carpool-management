using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpoolManagement.Persistance.Models
{
    public partial class RideShareEmployeeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RideShareId { get; set; }
        public int EmployeeId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual RideShareEntity RideShare { get; set; }
        public virtual EmployeeEntity Employee { get;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public class RideShareEmployeeEntityConfiguration : IEntityTypeConfiguration<RideShareEmployeeEntity>
        {
            public void Configure(EntityTypeBuilder<RideShareEmployeeEntity> entity)
            {
                entity.HasKey(entity => entity.Id);

                entity.ToTable("RideShareEmployee");

                entity.HasIndex(entity => entity.Id)
                    .HasDatabaseName("PK_RIDE_SHARE_EMPLOYEE")
                    .IsUnique();

                entity.Property(entity => entity.Id)
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                entity.Property(entity => entity.RideShareId)
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                entity.Property(entity => entity.EmployeeId)
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                entity.HasIndex(entity => entity.RideShareId)
                    .HasDatabaseName("IX_FK_RIDE_SHARE_EMPLOYEE");

                entity.HasIndex(entity => entity.EmployeeId)
                    .HasDatabaseName("IX_FK_RIDE_SHARE_EMPLOYEE_01");

                entity.HasOne(entity => entity.RideShare)
                    .WithMany(rideShare => rideShare.RideShareEmployee)
                    .HasForeignKey(entity => entity.RideShareId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RIDE_SHARE_EMPLOYEE");

                entity.HasOne(entity => entity.Employee)
                    .WithMany(employee => employee.RideShareEmployee)
                    .HasForeignKey(entity => entity.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RIDE_SHARE_EMPLOYEE_01");
            }
        }
    }
}
