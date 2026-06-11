using MediFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace MediFinder.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Doctor> Doctors => Set<Doctor>();

    public DbSet<Specialization> Specializations => Set<Specialization>();

    public DbSet<DoctorSpecialization> DoctorSpecializations => Set<DoctorSpecialization>();

    public DbSet<Area> Areas => Set<Area>();

    public DbSet<Clinic> Clinics => Set<Clinic>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<DoctorAvailability> DoctorAvailabilities => Set<DoctorAvailability>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DoctorSpecialization>()
            .HasKey(item => new { item.DoctorId, item.SpecializationId });

        modelBuilder.Entity<Doctor>()
            .HasIndex(doctor => doctor.Slug)
            .IsUnique();

        modelBuilder.Entity<Specialization>()
            .HasIndex(specialization => specialization.Slug)
            .IsUnique();

        modelBuilder.Entity<Area>()
            .HasIndex(area => new { area.City, area.Name });

        modelBuilder.Entity<Clinic>()
            .Property(clinic => clinic.ConsultationFee)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Doctor>()
            .Property(doctor => doctor.AverageRating)
            .HasPrecision(3, 2);

        modelBuilder.Entity<Clinic>()
            .Property(clinic => clinic.Latitude)
            .HasPrecision(9, 6);

        modelBuilder.Entity<Clinic>()
            .Property(clinic => clinic.Longitude)
            .HasPrecision(9, 6);

        modelBuilder.Entity<DoctorAvailability>()
            .HasOne(item => item.Doctor)
            .WithMany(doctor => doctor.Availabilities)
            .HasForeignKey(item => item.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<DoctorAvailability>()
            .HasOne(item => item.Clinic)
            .WithMany()
            .HasForeignKey(item => item.ClinicId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
