﻿// <auto-generated />
using System;
using GenshinFarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GenshinFarm.Api.Migrations
{
    [DbContext(typeof(GenshinDbContext))]
    [Migration("20201229224633_UserSecurity-Improoved")]
    partial class UserSecurityImprooved
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CharacterMaterial", b =>
                {
                    b.Property<string>("CharactersId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TalentMaterialsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CharactersId", "TalentMaterialsId");

                    b.HasIndex("TalentMaterialsId");

                    b.ToTable("CharacterMaterial");
                });

            modelBuilder.Entity("CharacterUser", b =>
                {
                    b.Property<string>("CharactersId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CharactersId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CharacterUser");
                });

            modelBuilder.Entity("DaysOfWeekMaterial", b =>
                {
                    b.Property<string>("DaysAvailableId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaterialsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DaysAvailableId", "MaterialsId");

                    b.HasIndex("MaterialsId");

                    b.ToTable("DaysOfWeekMaterial");
                });

            modelBuilder.Entity("FarmLocationMaterial", b =>
                {
                    b.Property<string>("FarmLocationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaterialsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FarmLocationId", "MaterialsId");

                    b.HasIndex("MaterialsId");

                    b.ToTable("FarmLocationMaterial");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.Character", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("PowerLvl")
                        .HasColumnType("int");

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Slug")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("WeaponType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.DaysOfWeek", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("DaysOfWeeks");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.FarmLocation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("Weekly")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("FarmLocations");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.Material", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Slug")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Materials");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Material");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.Talent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CharacterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("PowerLvl")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Talents");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("LastTimeLoged")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Salt")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Username")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.UserElement", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ElementId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lvl")
                        .HasColumnType("int");

                    b.Property<int>("PowerLvl")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserElement");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.Weapon", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<string>("Desciption")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("PowerLvl")
                        .HasColumnType("int");

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Slug")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("UserWeapon", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WeaponsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "WeaponsId");

                    b.HasIndex("WeaponsId");

                    b.ToTable("UserWeapon");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.AscMaterial", b =>
                {
                    b.HasBaseType("GenshinFarm.Core.Entities.Material");

                    b.Property<string>("CharacterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("CharacterId");

                    b.HasDiscriminator().HasValue("AscMaterial");
                });

            modelBuilder.Entity("CharacterMaterial", b =>
                {
                    b.HasOne("GenshinFarm.Core.Entities.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GenshinFarm.Core.Entities.Material", null)
                        .WithMany()
                        .HasForeignKey("TalentMaterialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterUser", b =>
                {
                    b.HasOne("GenshinFarm.Core.Entities.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GenshinFarm.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DaysOfWeekMaterial", b =>
                {
                    b.HasOne("GenshinFarm.Core.Entities.DaysOfWeek", null)
                        .WithMany()
                        .HasForeignKey("DaysAvailableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GenshinFarm.Core.Entities.Material", null)
                        .WithMany()
                        .HasForeignKey("MaterialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmLocationMaterial", b =>
                {
                    b.HasOne("GenshinFarm.Core.Entities.FarmLocation", null)
                        .WithMany()
                        .HasForeignKey("FarmLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GenshinFarm.Core.Entities.Material", null)
                        .WithMany()
                        .HasForeignKey("MaterialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.Talent", b =>
                {
                    b.HasOne("GenshinFarm.Core.Entities.Character", "Character")
                        .WithMany("Talents")
                        .HasForeignKey("CharacterId");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.UserElement", b =>
                {
                    b.HasOne("GenshinFarm.Core.Entities.User", "User")
                        .WithMany("UserElement")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserWeapon", b =>
                {
                    b.HasOne("GenshinFarm.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GenshinFarm.Core.Entities.Weapon", null)
                        .WithMany()
                        .HasForeignKey("WeaponsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.AscMaterial", b =>
                {
                    b.HasOne("GenshinFarm.Core.Entities.Character", null)
                        .WithMany("AscensionMaterials")
                        .HasForeignKey("CharacterId");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.Character", b =>
                {
                    b.Navigation("AscensionMaterials");

                    b.Navigation("Talents");
                });

            modelBuilder.Entity("GenshinFarm.Core.Entities.User", b =>
                {
                    b.Navigation("UserElement");
                });
#pragma warning restore 612, 618
        }
    }
}
