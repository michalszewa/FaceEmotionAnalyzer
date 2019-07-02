﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjektIPS.Models;

namespace ProjektIPS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190624190121_2mig")]
    partial class _2mig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjektIPS.Models.Face", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Age");

                    b.Property<string>("Emotion");

                    b.Property<string>("Gender");

                    b.Property<int>("Height");

                    b.Property<int>("ImageId");

                    b.Property<int>("Left");

                    b.Property<int>("Top");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Faces");
                });

            modelBuilder.Entity("ProjektIPS.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Anger");

                    b.Property<int>("Contempt");

                    b.Property<int>("Disgust");

                    b.Property<int>("Fear");

                    b.Property<int>("Happiness");

                    b.Property<int>("Neutral");

                    b.Property<string>("Path");

                    b.Property<int>("Sadness");

                    b.Property<int>("Surprise");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ProjektIPS.Models.Face", b =>
                {
                    b.HasOne("ProjektIPS.Models.Image", "Image")
                        .WithMany("Faces")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
