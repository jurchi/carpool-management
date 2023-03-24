using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpoolManagement.Persistance.Models
{
    public partial class EmployeeEntity
    {
        public EmployeeEntity()
        {
            RideShareEmployee = new HashSet<RideShareEmployeeEntity>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool IsDriver { get; set; }

        public virtual ICollection<RideShareEmployeeEntity> RideShareEmployee { get; set; }

        public class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>
        {
            public void Configure(EntityTypeBuilder<EmployeeEntity> entity)
            {
                entity.HasKey(entity => entity.Id);

                entity.ToTable("Employee");

                entity.HasIndex(entity => entity.Id)
                    .HasDatabaseName("PK_EMPLOYEE")
                    .IsUnique();

                entity.Property(entity => entity.Id)
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                entity.Property(entity => entity.Name)
                    .IsRequired()
                    .HasColumnType("TEXT");

                entity.Property(entity => entity.IsDriver)
                    .IsRequired()
                    .HasColumnType("INTEGER");
            }
        }
    }
}
