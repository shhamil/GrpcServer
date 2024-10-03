﻿// <auto-generated />
using GrpcServer.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GrpcServer.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241002192007_Initial_Migrate")]
    partial class Initial_Migrate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GrpcServer.Models.GrpcData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Decimal1")
                        .HasColumnType("double precision");

                    b.Property<double>("Decimal2")
                        .HasColumnType("double precision");

                    b.Property<double>("Decimal3")
                        .HasColumnType("double precision");

                    b.Property<double>("Decimal4")
                        .HasColumnType("double precision");

                    b.Property<int>("PacketSeqNum")
                        .HasColumnType("integer");

                    b.Property<string>("PacketTimestamp")
                        .HasColumnType("text");

                    b.Property<int>("RecordSeqNum")
                        .HasColumnType("integer");

                    b.Property<string>("RecordTimestamp")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("grpc_data");
                });
#pragma warning restore 612, 618
        }
    }
}
