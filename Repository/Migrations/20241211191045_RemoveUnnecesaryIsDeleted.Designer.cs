﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository_CodeFirst;

#nullable disable

namespace Repository_CodeFirst.Migrations
{
    [DbContext(typeof(FitnessDBContext))]
    [Migration("20241211191045_RemoveUnnecesaryIsDeleted")]
    partial class RemoveUnnecesaryIsDeleted
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LibrarieModele.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExerciseId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DifficultyLevel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("VideoUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ExerciseId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("LibrarieModele.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateTime>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("PaymentId");

                    b.HasIndex("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("LibrarieModele.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubscriptionId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateTime>("StartDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<string>("SubscriptionType")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("LibrarieModele.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FitnessLevel")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("RegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<DateTime?>("TrialExpiration")
                        .HasColumnType("date");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LibrarieModele.WorkoutPlan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlanId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("DurationWeeks")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("FitnessLevel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("PlanId");

                    b.ToTable("WorkoutPlans");
                });

            modelBuilder.Entity("LibrarieModele.WorkoutPlanExercise", b =>
                {
                    b.Property<int>("PlanId")
                        .HasColumnType("integer");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("integer");

                    b.HasKey("PlanId", "ExerciseId");

                    b.HasIndex("ExerciseId");

                    b.ToTable("WorkoutPlanExercises");
                });

            modelBuilder.Entity("LibrarieModele.Payment", b =>
                {
                    b.HasOne("LibrarieModele.Subscription", "Subscription")
                        .WithMany("Payments")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibrarieModele.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibrarieModele.Subscription", b =>
                {
                    b.HasOne("LibrarieModele.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibrarieModele.WorkoutPlanExercise", b =>
                {
                    b.HasOne("LibrarieModele.Exercise", "Exercise")
                        .WithMany("WorkoutPlanExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibrarieModele.WorkoutPlan", "WorkoutPlan")
                        .WithMany("WorkoutPlanExercises")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("WorkoutPlan");
                });

            modelBuilder.Entity("LibrarieModele.Exercise", b =>
                {
                    b.Navigation("WorkoutPlanExercises");
                });

            modelBuilder.Entity("LibrarieModele.Subscription", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("LibrarieModele.User", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("LibrarieModele.WorkoutPlan", b =>
                {
                    b.Navigation("WorkoutPlanExercises");
                });
#pragma warning restore 612, 618
        }
    }
}