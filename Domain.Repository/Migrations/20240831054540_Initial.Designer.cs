﻿// <auto-generated />
using System;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Repository.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240831054540_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Repository.Quizes.AnswerOptionDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("QuizItemDtoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuizItemDtoId");

                    b.ToTable("AnswerOptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Answer = "1992",
                            QuizItemDtoId = 1
                        },
                        new
                        {
                            Id = 2,
                            Answer = "1991",
                            QuizItemDtoId = 1
                        },
                        new
                        {
                            Id = 3,
                            Answer = "1994",
                            QuizItemDtoId = 1
                        },
                        new
                        {
                            Id = 4,
                            Answer = "1995",
                            QuizItemDtoId = 1
                        },
                        new
                        {
                            Id = 5,
                            Answer = "Вася",
                            QuizItemDtoId = 2
                        },
                        new
                        {
                            Id = 6,
                            Answer = "Петя",
                            QuizItemDtoId = 2
                        },
                        new
                        {
                            Id = 7,
                            Answer = "Олег",
                            QuizItemDtoId = 2
                        },
                        new
                        {
                            Id = 8,
                            Answer = "Иван",
                            QuizItemDtoId = 2
                        });
                });

            modelBuilder.Entity("Domain.Repository.Quizes.GivenAnswerDto", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("QuestionId"));

                    b.Property<int>("AnswerId")
                        .HasColumnType("integer");

                    b.HasKey("QuestionId");

                    b.ToTable("GivenAnswers");
                });

            modelBuilder.Entity("Domain.Repository.Quizes.QuizDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Quizes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Первая викторина"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Вторая"
                        });
                });

            modelBuilder.Entity("Domain.Repository.Quizes.QuizItemDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CorrectAnswerId")
                        .HasColumnType("integer");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("QuizDtoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuizDtoId");

                    b.ToTable("QuizItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CorrectAnswerId = 2,
                            Question = "Когда я родился",
                            QuizDtoId = 1
                        },
                        new
                        {
                            Id = 2,
                            CorrectAnswerId = 6,
                            Question = "Как меня зовут",
                            QuizDtoId = 2
                        });
                });

            modelBuilder.Entity("Domain.Repository.Quizes.QuizStateDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CurrentQuestion")
                        .HasColumnType("integer");

                    b.Property<int>("Quiz")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("QuizStates");
                });

            modelBuilder.Entity("Domain.Repository.Security.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(400)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "test@test.test",
                            Name = "test",
                            Password = "test",
                            Role = "test"
                        });
                });

            modelBuilder.Entity("Domain.Repository.Quizes.AnswerOptionDto", b =>
                {
                    b.HasOne("Domain.Repository.Quizes.QuizItemDto", "QuizItemDto")
                        .WithMany("AnswerOptions")
                        .HasForeignKey("QuizItemDtoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuizItemDto");
                });

            modelBuilder.Entity("Domain.Repository.Quizes.QuizItemDto", b =>
                {
                    b.HasOne("Domain.Repository.Quizes.QuizDto", "QuizDto")
                        .WithMany("QuizItemDtos")
                        .HasForeignKey("QuizDtoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuizDto");
                });

            modelBuilder.Entity("Domain.Repository.Quizes.QuizDto", b =>
                {
                    b.Navigation("QuizItemDtos");
                });

            modelBuilder.Entity("Domain.Repository.Quizes.QuizItemDto", b =>
                {
                    b.Navigation("AnswerOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
