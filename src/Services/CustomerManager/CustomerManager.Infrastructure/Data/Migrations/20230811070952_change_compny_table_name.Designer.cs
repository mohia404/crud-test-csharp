﻿// <auto-generated />
using System;
using CustomerManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomerManager.Infrastructure.Data.Migrations
{
    [DbContext(typeof(CustomerManagerDbContext))]
    [Migration("20230811070952_change_compny_table_name")]
    partial class change_compny_table_name
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomerManager.Domain.Companies.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("CustomerManager.Domain.Companies.Company", b =>
                {
                    b.OwnsMany("CustomerManager.Domain.Companies.Entities.Customer", "Customers", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("BankAccountNumber")
                                .IsRequired()
                                .HasMaxLength(24)
                                .IsUnicode(false)
                                .HasColumnType("varchar(24)");

                            b1.Property<DateTime>("DateOfBirth")
                                .HasColumnType("datetime");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasMaxLength(100)
                                .IsUnicode(false)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Firstname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Lastname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<long>("PhoneNumber")
                                .HasColumnType("bigint");

                            b1.HasKey("Id", "CompanyId");

                            b1.HasIndex("CompanyId");

                            b1.ToTable("Customers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
