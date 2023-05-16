﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CleanArchitecture.Infrastructure.Database;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    [DbContext(typeof(CleanArchitecturesDbContext))]
    partial class CleanArchitecturesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("CleanArchitecture.Domain.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(2048)");

                    b.Property<string>("Ean")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Models.ProductPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimalcls(15,3)");

                    b.Property<DateTime>("PriceForDate")
                        .HasColumnType("date");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SellerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SellerId");

                    b.ToTable("ProductPrices");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Models.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Sellers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "SellerOne"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "SellerTwo"
                        });
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Models.ProductPrice", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Models.Product", "Product")
                        .WithMany("ProductPrices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Models.Seller", "Seller")
                        .WithMany("ProductPrices")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Models.Product", b =>
                {
                    b.Navigation("ProductPrices");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Models.Seller", b =>
                {
                    b.Navigation("ProductPrices");
                });
#pragma warning restore 612, 618
        }
    }
}
