﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TODOList.API.Data;

#nullable disable

namespace TODOList.API.Migrations
{
    [DbContext(typeof(TodoListDbContext))]
    [Migration("20230804151802_m2m")]
    partial class m2m
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TODOList.API.Models.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6b55a90f-ea79-4e85-accf-540be8f2e2a0"),
                            Name = "Work"
                        },
                        new
                        {
                            Id = new Guid("12ccd14e-21af-4801-bf14-e80d6b1a2ff5"),
                            Name = "Pet"
                        },
                        new
                        {
                            Id = new Guid("4521d9f7-c05f-4f76-aa18-6fba6b70e1bc"),
                            Name = "Personal"
                        },
                        new
                        {
                            Id = new Guid("1ee26f62-463f-4125-801f-7fe91ae329ba"),
                            Name = "Finance"
                        },
                        new
                        {
                            Id = new Guid("a65d0695-4f8b-49a7-a09a-5e0ec06a142e"),
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("TODOList.API.Models.Domain.Category_Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TodoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TodoId");

                    b.ToTable("Category_Todos");
                });

            modelBuilder.Entity("TODOList.API.Models.Domain.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TODOList.API.Models.Domain.Category_Todo", b =>
                {
                    b.HasOne("TODOList.API.Models.Domain.Category", "Category")
                        .WithMany("Category_Todos")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TODOList.API.Models.Domain.Todo", "Todo")
                        .WithMany("Category_Todos")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Todo");
                });

            modelBuilder.Entity("TODOList.API.Models.Domain.Category", b =>
                {
                    b.Navigation("Category_Todos");
                });

            modelBuilder.Entity("TODOList.API.Models.Domain.Todo", b =>
                {
                    b.Navigation("Category_Todos");
                });
#pragma warning restore 612, 618
        }
    }
}
