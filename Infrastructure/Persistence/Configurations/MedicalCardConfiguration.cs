using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MedicalCardConfiguration : IEntityTypeConfiguration<MedicalCard>
{
    public void Configure(EntityTypeBuilder<MedicalCard> builder)
    {
        builder.ToTable("MedicalCards");
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.UserId);
        
        builder.Property(x => x.Name).IsRequired();
    }
}
