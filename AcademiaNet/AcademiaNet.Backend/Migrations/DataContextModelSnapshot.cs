﻿// <auto-generated />
using AcademiaNet.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcademiaNet.Backend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcademiaNet.Shared.Entites.AcademicProgram", b =>
                {
                    b.Property<int>("AcademicProgramID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcademicProgramID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("InstitutionID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AcademicProgramID");

                    b.HasIndex("InstitutionID");

                    b.HasIndex("AcademicProgramID", "Name")
                        .IsUnique();

                    b.ToTable("AcademicPrograms", (string)null);
                });

            modelBuilder.Entity("AcademiaNet.Shared.Entites.Institution", b =>
                {
                    b.Property<int>("InstitutionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstitutionID"));

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("InstitutionID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Institutions", (string)null);
                });

            modelBuilder.Entity("AcademiaNet.Shared.Entites.AcademicProgram", b =>
                {
                    b.HasOne("AcademiaNet.Shared.Entites.Institution", "Institution")
                        .WithMany("AcademicPrograms")
                        .HasForeignKey("InstitutionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("AcademiaNet.Shared.Entites.Institution", b =>
                {
                    b.Navigation("AcademicPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
