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
            .ToTable("Companies");

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
            .OwnsMany(s => s.Customers, customerBuilder =>
            {
                customerBuilder
                    .ToTable("Customers");

                customerBuilder
                    .Property(d => d.Id)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => CustomerId.Create(value));  
                
                customerBuilder
                    .Property(d => d.PhoneNumber)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => CustomerPhoneNumber.Create(value));

                customerBuilder
                    .WithOwner()
                    .HasForeignKey("CompanyId");


                customerBuilder.HasKey("Id", "CompanyId");

                customerBuilder
                    .Property(x=>x.Firstname)
                    .HasMaxLength(100);

                customerBuilder
                    .Property(x => x.Lastname)
                    .HasMaxLength(100);

                customerBuilder
                    .Property(x => x.DateOfBirth)
                    .HasColumnType("datetime");

                customerBuilder
                    .Property(x => x.PhoneNumber)
                    .HasMaxLength(20);

                customerBuilder
                    .Property(x => x.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                customerBuilder
                    .Property(x => x.BankAccountNumber)
                    .HasMaxLength(24)
                    .IsUnicode(false);
            });

        builder.Metadata
            .FindNavigation(nameof(Company.Customers))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}