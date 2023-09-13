using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Data.Mappings;

public class ChooseRestaurantMap : IEntityTypeConfiguration<ChooseRestaurant>
{
    public void Configure(EntityTypeBuilder<ChooseRestaurant> builder)
    {
        builder.ToTable("ChooseRestaurant");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.DateVoting)
            .IsRequired()
            .HasColumnName("DataVoting")
            .HasColumnType("DATETIME");

        builder.Property(x => x.WeekChoose)
            .IsRequired()
            .HasColumnName("WeekChoose")
            .HasColumnType("INT");
    }
}
