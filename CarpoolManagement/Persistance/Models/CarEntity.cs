using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpoolManagement.Persistance.Models
{
    public partial class CarEntity
    {
        public CarEntity()
        {
            RideShare = new HashSet<RideShareEntity>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Unique]
        public string? Plate { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }

        public string? Color { get; set; }

        public int NumberOfSeats { get; set; }

        public virtual ICollection<RideShareEntity> RideShare { get; set; }

        public class CarEntityConfiguration : IEntityTypeConfiguration<CarEntity>
        {
            public void Configure(EntityTypeBuilder<CarEntity> entity)
            {
                entity.HasKey(entity => entity.Id);

                entity.ToTable("Car");

                entity.HasIndex(entity => entity.Id)
                    .HasDatabaseName("PK_CAR")
                    .IsUnique();

                entity.Property(entity => entity.Id)
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                entity.Property(entity => entity.Plate)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(entity => entity.Name)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(entity => entity.Type)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(entity => entity.Color)
                    .IsRequired()
                    .HasColumnType("TEXT");
            }
        }
    }
}