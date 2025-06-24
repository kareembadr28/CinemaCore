using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using training_EFCore.models;

namespace training_EFCore.configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t=> t.ID);
            builder.Property(t => t.ID).ValueGeneratedOnAdd();
            builder.Property(t => t.SeatNumber).IsRequired();
            builder.Property(t => t.Price).IsRequired();
            builder.Property(t => t.CustomerName).IsRequired();


        }
    }
}
