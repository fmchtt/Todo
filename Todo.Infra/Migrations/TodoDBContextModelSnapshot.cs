﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Todo.Infra.Contexts;

#nullable disable

namespace Todo.Infra.Migrations
{
    [DbContext(typeof(TodoDBContext))]
    partial class TodoDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BoardUser", b =>
                {
                    b.Property<Guid>("BoardsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParticipantsId")
                        .HasColumnType("uuid");

                    b.HasKey("BoardsId", "ParticipantsId");

                    b.HasIndex("ParticipantsId");

                    b.ToTable("BoardUser");
                });

            modelBuilder.Entity("Todo.Domain.Entities.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Todo.Domain.Entities.Column", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("Order", "BoardId")
                        .IsUnique();

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("Todo.Domain.Entities.RecoverCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("UserId", "Code")
                        .IsUnique();

                    b.ToTable("RecoverCodes");
                });

            modelBuilder.Entity("Todo.Domain.Entities.TodoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ColumnId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("ColumnId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Itens");
                });

            modelBuilder.Entity("Todo.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BoardUser", b =>
                {
                    b.HasOne("Todo.Domain.Entities.Board", null)
                        .WithMany()
                        .HasForeignKey("BoardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Todo.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Todo.Domain.Entities.Board", b =>
                {
                    b.HasOne("Todo.Domain.Entities.User", "Owner")
                        .WithMany("OwnedBoards")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Todo.Domain.Entities.Column", b =>
                {
                    b.HasOne("Todo.Domain.Entities.Board", "Board")
                        .WithMany("Columns")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("Todo.Domain.Entities.RecoverCode", b =>
                {
                    b.HasOne("Todo.Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Todo.Domain.Entities.RecoverCode", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Todo.Domain.Entities.TodoItem", b =>
                {
                    b.HasOne("Todo.Domain.Entities.Board", "Board")
                        .WithMany("Itens")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Todo.Domain.Entities.Column", "Column")
                        .WithMany("Itens")
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Todo.Domain.Entities.User", "Creator")
                        .WithMany("Itens")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("Column");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Todo.Domain.Entities.Board", b =>
                {
                    b.Navigation("Columns");

                    b.Navigation("Itens");
                });

            modelBuilder.Entity("Todo.Domain.Entities.Column", b =>
                {
                    b.Navigation("Itens");
                });

            modelBuilder.Entity("Todo.Domain.Entities.User", b =>
                {
                    b.Navigation("Itens");

                    b.Navigation("OwnedBoards");
                });
#pragma warning restore 612, 618
        }
    }
}
