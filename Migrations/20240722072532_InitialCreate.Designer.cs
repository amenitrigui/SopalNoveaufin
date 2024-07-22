﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SopalS.Data;

#nullable disable

namespace SopalS.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240722072532_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SopalS.Models.ConteneurModel.Conteneur", b =>
                {
                    b.Property<int>("Ref")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Ref"));

                    b.Property<string>("CodeBarres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDernierEtalonnage")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateMiseEnService")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<double>("DernierPoids")
                        .HasColumnType("float");

                    b.Property<int>("EmplacementId")
                        .HasColumnType("int");

                    b.Property<int>("PeriodiciteEtalonnage")
                        .HasColumnType("int");

                    b.Property<string>("Unite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserUpdate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Ref");

                    b.HasIndex("EmplacementId");

                    b.ToTable("Conteneur");
                });

            modelBuilder.Entity("SopalS.Models.Emplacement", b =>
                {
                    b.Property<int>("Codeemp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codeemp"));

                    b.Property<string>("libele")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codeemp");

                    b.ToTable("Emplacement");
                });

            modelBuilder.Entity("SopalS.Models.HistoEtalonnageModel.HistoEtalonnage", b =>
                {
                    b.Property<int>("Ref")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Ref"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Poids")
                        .HasColumnType("real");

                    b.Property<string>("Unite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Ref");

                    b.ToTable("HistoEtalonnages");
                });

            modelBuilder.Entity("SopalS.Models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Utilisateur");
                });

            modelBuilder.Entity("SopalS.Models.ConteneurModel.Conteneur", b =>
                {
                    b.HasOne("SopalS.Models.Emplacement", "Emplacement")
                        .WithMany()
                        .HasForeignKey("EmplacementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emplacement");
                });
#pragma warning restore 612, 618
        }
    }
}