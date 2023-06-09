﻿// <auto-generated />
using System;
using ICS.Workout;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ICS.Workout.Migrations
{
    [DbContext(typeof(WorkoutDbContext))]
    partial class WorkoutDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ICS.Workout.Routine", b =>
                {
                    b.Property<Guid>("RoutineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoutineId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("RoutineId"), false);

                    b.HasAlternateKey("RoutineId", "Position");

                    b.HasIndex("WorkoutId", "Position");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("WorkoutId", "Position"));

                    b.ToTable("Routine", "Workout");
                });

            modelBuilder.Entity("ICS.Workout.Set", b =>
                {
                    b.Property<Guid>("SetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<Guid>("RoutineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("SetId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("SetId"), false);

                    b.HasAlternateKey("SetId", "Position");

                    b.HasIndex("RoutineId", "Position");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("RoutineId", "Position"));

                    b.ToTable("Set", "Workout");
                });

            modelBuilder.Entity("ICS.Workout.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.ToTable("User", "Workout");
                });

            modelBuilder.Entity("ICS.Workout.Workout", b =>
                {
                    b.Property<Guid>("WorkoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WorkoutId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("WorkoutId"), false);

                    b.HasAlternateKey("WorkoutId", "Position");

                    b.HasIndex("UserId", "Position");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("UserId", "Position"));

                    b.ToTable("Workout", "Workout");
                });

            modelBuilder.Entity("ICS.Workout.Routine", b =>
                {
                    b.HasOne("ICS.Workout.Workout", "Workout")
                        .WithMany("Routines")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("ICS.Workout.Set", b =>
                {
                    b.HasOne("ICS.Workout.Routine", "Routine")
                        .WithMany("Sets")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Routine");
                });

            modelBuilder.Entity("ICS.Workout.Workout", b =>
                {
                    b.HasOne("ICS.Workout.User", "User")
                        .WithMany("Workouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ICS.Workout.Routine", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("ICS.Workout.User", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("ICS.Workout.Workout", b =>
                {
                    b.Navigation("Routines");
                });
#pragma warning restore 612, 618
        }
    }
}
