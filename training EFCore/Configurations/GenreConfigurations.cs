using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using training_EFCore.models;

namespace training_EFCore.configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.ID);
            builder.Property(g => g.ID).ValueGeneratedOnAdd();
            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(50);



        }
    }
}
