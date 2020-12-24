﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoroRestApi.DataAccess;

namespace PhoroRestApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201127142300_AddedManyToManyRelBetweenAlbumsAndPhotos_Migration1")]
    partial class AddedManyToManyRelBetweenAlbumsAndPhotos_Migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PhoroRestApi.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("PhoroRestApi.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(250) CHARACTER SET utf8mb4")
                        .HasMaxLength(250);

                    b.Property<string>("Location")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("PhoroRestApi.Models.AlbumPhoto", b =>
                {
                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("PhotoId")
                        .HasColumnType("int");

                    b.HasKey("AlbumId", "PhotoId");

                    b.HasIndex("PhotoId");

                    b.ToTable("AlbumsPhotos");
                });

            modelBuilder.Entity("PhoroRestApi.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<int>("ToUserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("PhoroRestApi.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<string>("DiskName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Location")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("PhoroRestApi.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(40) CHARACTER SET utf8mb4")
                        .HasMaxLength(40);

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("PhoroRestApi.Models.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CoverPicId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(250) CHARACTER SET utf8mb4")
                        .HasMaxLength(250);

                    b.Property<string>("Location")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(15) CHARACTER SET utf8mb4")
                        .HasMaxLength(15);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ProfilePicId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("PhoroRestApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<DateTime>("RegisterDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(40) CHARACTER SET utf8mb4")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PhoroRestApi.Models.Album", b =>
                {
                    b.HasOne("PhoroRestApi.Models.Seller", "Seller")
                        .WithMany("Albums")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhoroRestApi.Models.AlbumPhoto", b =>
                {
                    b.HasOne("PhoroRestApi.Models.Album", "Album")
                        .WithMany("AlbumPhotos")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoroRestApi.Models.Photo", "Photo")
                        .WithMany("AlbumPhotos")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhoroRestApi.Models.Message", b =>
                {
                    b.HasOne("PhoroRestApi.Models.User", null)
                        .WithMany("Messages")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PhoroRestApi.Models.Photo", b =>
                {
                    b.HasOne("PhoroRestApi.Models.Seller", null)
                        .WithMany("Photos")
                        .HasForeignKey("SellerId");
                });

            modelBuilder.Entity("PhoroRestApi.Models.Review", b =>
                {
                    b.HasOne("PhoroRestApi.Models.Seller", "Seller")
                        .WithMany("Reviews")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoroRestApi.Models.User", null)
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
