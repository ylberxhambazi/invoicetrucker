using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetForge.Api.Persistence.Configurations;

internal sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ConfigureEntityBase();
        builder.Property(expense => expense.Category).HasStringConversion();
        builder.Property(expense => expense.Date).HasColumnType("date");
        builder.Property(expense => expense.Supplier).HasMaxLength(160).IsRequired();
        builder.Property(expense => expense.Description).HasMaxLength(300).IsRequired();
        builder.Property(expense => expense.Amount).HasPrecision(18, 2);
        builder.Property(expense => expense.Currency).HasMaxLength(3).IsFixedLength().IsRequired();

        builder.HasIndex(expense => expense.Date);
        builder.HasIndex(expense => expense.Category);
        builder.HasIndex(expense => new { expense.TruckId, expense.Date });

        builder.HasOne(expense => expense.Truck)
            .WithMany(truck => truck.Expenses)
            .HasForeignKey(expense => expense.TruckId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable(table =>
            table.HasCheckConstraint("CK_Expenses_Amount", "\"Amount\" > 0"));
    }
}
