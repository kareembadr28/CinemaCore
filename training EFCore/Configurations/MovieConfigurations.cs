using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using training_EFCore.models;

namespace training_EFCore.configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.ID);
           builder.Property(m=>m.ID).ValueGeneratedOnAdd();
            builder.Property(m => m.Name).IsRequired().HasMaxLength(150);
            builder.Property(m => m.Description).IsRequired().HasMaxLength(200);

            
        }
    }
}
