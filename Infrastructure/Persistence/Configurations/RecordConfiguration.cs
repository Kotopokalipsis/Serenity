using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RecordConfiguration : IEntityTypeConfiguration<Record>
{
    public void Configure(EntityTypeBuilder<Record> builder)
    {
        builder.ToTable("Records");
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.MedicalCardId);
        builder.HasIndex(x => x.ServiceTypeId);
        builder.HasIndex(x => x.ServiceCategoryId);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}
