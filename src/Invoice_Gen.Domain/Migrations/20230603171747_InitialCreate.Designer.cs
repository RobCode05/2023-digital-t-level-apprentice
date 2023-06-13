﻿// <auto-generated />
using Invoice_Gen.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Invoice_Gen.Domain.Migrations
{
    [DbContext(typeof(InvoiceGenDbContext))]
    [Migration("20230603171747_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Invoice_Gen.Domain.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ClientId");

                    b.ToTable("Clients", (string)null);

                    b.HasData(
                        new
                        {
                            ClientId = 1,
                            ClientAddress = "4509 Clement Street, Atlanta, GA 30331",
                            ClientName = "Muller Inc",
                            ContactEmail = "AaronDBaker@dayrep.com",
                            ContactName = "Bradley S Crooks"
                        },
                        new
                        {
                            ClientId = 2,
                            ClientAddress = "2545 James Street, Fairport, NY 14450",
                            ClientName = "Gutkowski Inc",
                            ContactEmail = "AnnaLWitt@teleworm.us",
                            ContactName = "Anna L. Witt"
                        },
                        new
                        {
                            ClientId = 3,
                            ClientAddress = "923 Euclid Avenue, San Luis Obispo, CA 93401",
                            ClientName = "Hoeger - Gislason",
                            ContactEmail = "SusanMRailey@armyspy.com",
                            ContactName = "Susan Railey"
                        },
                        new
                        {
                            ClientId = 4,
                            ClientAddress = "2607 Goldcliff Circle, Washington, DC 20005",
                            ClientName = "Toy Group",
                            ContactEmail = "StanleyDRogers@dayrep.com",
                            ContactName = "Stanley D. Rogers"
                        },
                        new
                        {
                            ClientId = 5,
                            ClientAddress = "1381 Monroe Street, Houston, TX 77030",
                            ClientName = "Upton, Gleason and Cronin",
                            ContactEmail = "TammyWFinley@dayrep.com",
                            ContactName = "Tammy W. Finley"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}