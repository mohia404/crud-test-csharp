using CustomerManager.Domain.Companies;
using CustomerManager.Domain.Companies.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerManager.Infrastructure.Data.Config;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        ConfigurePhonebooksTable(builder);
        ConfigureCustomersTable(builder);
    }

    private static void ConfigurePhonebooksTable(EntityTypeBuilder<Company> builder)
    {
        builder
            .ToTable("companies");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(d => d.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CompanyId.Create(value));
    }

    private static void ConfigureCustomersTable(EntityTypeBuilder<Company> builder)
    {
        builder
            .OwnsMany(s => s.Customers, boxBuilder =>
            {
                boxBuilder
                    .ToTable("Customers");

                boxBuilder
                    .Property(d => d.Id)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => CustomerId.Create(value));

                boxBuilder
                    .WithOwner()
                    .HasForeignKey("CompanyId");


                boxBuilder.HasKey("Id", "CompanyId");
            });

        builder.Metadata
            .FindNavigation(nameof(Company.Customers))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}