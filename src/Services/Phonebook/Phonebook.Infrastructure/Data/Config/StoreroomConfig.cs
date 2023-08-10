using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Phonebook.Domain.Phonebooks.ValueObjects;

namespace Phonebook.Infrastructure.Data.Config;

public class PhonebookConfig : IEntityTypeConfiguration<Domain.Phonebooks.Phonebook>
{
    public void Configure(EntityTypeBuilder<Domain.Phonebooks.Phonebook> builder)
    {
        ConfigurePhonebooksTable(builder);
        ConfigureCustomersTable(builder);
    }

    private static void ConfigurePhonebooksTable(EntityTypeBuilder<Domain.Phonebooks.Phonebook> builder)
    {
        builder
            .ToTable("Phonebooks");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(d => d.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => PhonebookId.Create(value));
    }

    private static void ConfigureCustomersTable(EntityTypeBuilder<Domain.Phonebooks.Phonebook> builder)
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
                    .HasForeignKey("PhonebookId");


                boxBuilder.HasKey("Id", "PhonebookId");
            });

        builder.Metadata
            .FindNavigation(nameof(Domain.Phonebooks.Phonebook.Customers))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}