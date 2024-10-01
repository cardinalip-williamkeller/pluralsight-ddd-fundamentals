using ClinicManagement.Domain.Aggregates.ClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Data.Config
{
  public class PatientConfiguration : IEntityTypeConfiguration<Patient>
  {
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
      builder
        .ToTable("Patients").HasKey(k => k.Id);
      builder
        .OwnsOne(p => p.AnimalValueObject, p =>
        {
          p.Property(pp => pp.Breed).HasColumnName("AnimalValueObject_Breed").HasMaxLength(50);
          p.Property(pp => pp.Species).HasColumnName("AnimalValueObject_Species").HasMaxLength(50);
        });

      builder.Metadata.FindNavigation(nameof(Patient.AnimalValueObject))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
  }
}
