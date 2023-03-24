using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpoolManagement.Persistance.Models
{
    public partial class RideShareEntity
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public RideShareEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            RideShareEmployee = new HashSet<RideShareEmployeeEntity>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? StartLocation { get; set; }

        public string? EndLocation { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DriverId { get; set; }

        public int CarId { get; set; }

        public virtual CarEntity Car { get; set; }

        public virtual ICollection<RideShareEmployeeEntity> RideShareEmployee { get; set; }

        public class RideShareEntityConfiguration : IEntityTypeConfiguration<RideShareEntity>
        {
            public void Configure(EntityTypeBuilder<RideShareEntity> entity)
            {
                entity.HasKey(entity => entity.Id);

                entity.ToTable("RideShare");

                entity.HasIndex(entity => entity.Id)
                    .HasDatabaseName("PK_RIDE_SHARE")
                    .IsUnique();

                entity.HasIndex(entity => entity.CarId)
                    .HasDatabaseName("FK_CAR_ID");

                entity.Property(entity => entity.Id)
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                entity.Property(entity => entity.DriverId)
                    .IsRequired()
                    .HasColumnType("INTEGER");

                entity.Property(entity => entity.EndDate)
                    .IsRequired()
                    .HasColumnType("DATE");

                entity.Property(entity => entity.StartDate)
                    .IsRequired()
                    .HasColumnType("DATE");

                entity.Property(entity => entity.StartLocation)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(entity => entity.EndLocation)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.HasOne(e => e.Car)
                    .WithMany(c => c.RideShare)
                    .HasForeignKey(e => e.CarId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CAR_ID");
            }
        }
    }
}
