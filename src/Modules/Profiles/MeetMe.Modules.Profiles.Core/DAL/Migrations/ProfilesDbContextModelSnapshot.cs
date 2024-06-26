﻿// <auto-generated />
using System;
using MeetMe.Modules.Profiles.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetMe.Modules.Profiles.Core.DAL.Migrations
{
    [DbContext(typeof(ProfilesDbContext))]
    partial class ProfilesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("profiles")
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MeetMe.Modules.Profiles.Core.Entities.Interest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("ProfileOwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProfileOwnerId");

                    b.ToTable("Interests", "profiles");
                });

            modelBuilder.Entity("MeetMe.Modules.Profiles.Core.Entities.Profile", b =>
                {
                    b.Property<Guid>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long?>("Age")
                        .HasColumnType("bigint");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("OwnerId");

                    b.ToTable("Profiles", "profiles");
                });

            modelBuilder.Entity("MeetMe.Modules.Profiles.Core.Entities.ProfileImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("BinaryData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<long>("DisplayOrder")
                        .HasMaxLength(100)
                        .HasColumnType("bigint");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProfileOwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("ProfileOwnerId");

                    b.ToTable("ProfileImages", "profiles");
                });

            modelBuilder.Entity("MeetMe.Modules.Profiles.Core.Entities.Interest", b =>
                {
                    b.HasOne("MeetMe.Modules.Profiles.Core.Entities.Profile", null)
                        .WithMany("Interests")
                        .HasForeignKey("ProfileOwnerId");
                });

            modelBuilder.Entity("MeetMe.Modules.Profiles.Core.Entities.ProfileImage", b =>
                {
                    b.HasOne("MeetMe.Modules.Profiles.Core.Entities.Profile", null)
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeetMe.Modules.Profiles.Core.Entities.Profile", null)
                        .WithMany("Images")
                        .HasForeignKey("ProfileOwnerId");
                });

            modelBuilder.Entity("MeetMe.Modules.Profiles.Core.Entities.Profile", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Interests");
                });
#pragma warning restore 612, 618
        }
    }
}
