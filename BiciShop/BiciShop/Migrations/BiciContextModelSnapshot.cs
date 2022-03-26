﻿// <auto-generated />
using BiciShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BiciShop.Migrations
{
    [DbContext(typeof(BiciContext))]
    partial class BiciContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BiciShop.Models.BiciType", b =>
                {
                    b.Property<int>("BiciTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BiciTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BiciTypeId");

                    b.ToTable("BiciTypes");
                });

            modelBuilder.Entity("BiciShop.Models.Bicicleta", b =>
                {
                    b.Property<int>("BicicletaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BiciTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("IssueYear")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<float>("WheelDiameter")
                        .HasColumnType("real");

                    b.HasKey("BicicletaId");

                    b.HasIndex("BiciTypeId");

                    b.ToTable("Bicicletas");
                });

            modelBuilder.Entity("BiciShop.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BicicletaId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("BicicletaId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BiciShop.Models.Bicicleta", b =>
                {
                    b.HasOne("BiciShop.Models.BiciType", "BiciType")
                        .WithMany()
                        .HasForeignKey("BiciTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BiciType");
                });

            modelBuilder.Entity("BiciShop.Models.Order", b =>
                {
                    b.HasOne("BiciShop.Models.Bicicleta", "Bicicleta")
                        .WithMany()
                        .HasForeignKey("BicicletaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bicicleta");
                });
#pragma warning restore 612, 618
        }
    }
}