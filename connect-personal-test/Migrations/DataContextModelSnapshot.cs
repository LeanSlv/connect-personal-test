﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using connect_personal_test.Models;

#nullable disable

namespace connect_personal_test.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("connect_personal_test.Models.Storage.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("ИД записи");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Наименование");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_UQ_Categories_Name");

                    b.ToTable("Category", "dbo");

                    b.HasComment("Категория");
                });

            modelBuilder.Entity("connect_personal_test.Models.Storage.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("ИД записи");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Описание");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Наименование");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Значение");

                    b.HasKey("Id");

                    b.ToTable("Order", "dbo");

                    b.HasComment("Заказчики");
                });

            modelBuilder.Entity("connect_personal_test.Models.Storage.OrderCategory", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasComment("ИД заказа");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasComment("ИД категории");

                    b.HasKey("OrderId", "CategoryId");

                    b.ToTable("OrderCategory", "dbo");

                    b.HasComment("Категория заказа");
                });
#pragma warning restore 612, 618
        }
    }
}
