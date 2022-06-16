using Microsoft.EntityFrameworkCore;
using System;

namespace cw8_mp_s22077.Models
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        internal object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescriptions_Medicaments { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.BirthDate).IsRequired();
                p.HasMany(e => e.Prescriptions).WithOne(e => e.Patient).HasForeignKey(e => e.IdPatient);

                p.HasData(
                    new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = new System.DateTime(1980, 1, 1) },
                    new Patient { IdPatient = 2, FirstName = "Anna", LastName = "Nowak", BirthDate = new System.DateTime(1981, 1, 1) },
                    new Patient { IdPatient = 3, FirstName = "Paweł", LastName = "Kowalski", BirthDate = new System.DateTime(1989, 3, 2) }
                );
            });

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);
                d.HasMany(e => e.Prescriptions).WithOne(e => e.Doctor).HasForeignKey(e => e.IdDoctor);

                d.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Nowak", Email = "a@a.pl" },
                    new Doctor { IdDoctor = 2, FirstName = "Anna", LastName = "Kowalska", Email = "b@b.pl" },
                    new Doctor { IdDoctor = 3, FirstName = "Paweł", LastName = "Robak", Email = "c@c.pl" }
                );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();

                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription { IdPrescription = 1, Date = new System.DateTime(2020, 1, 4), DueDate = new System.DateTime(2020, 5, 8), IdPatient = 1, IdDoctor = 1 },
                    new Prescription { IdPrescription = 2, Date = new System.DateTime(2020, 2, 1), DueDate = new System.DateTime(2020, 6, 7), IdPatient = 2, IdDoctor = 2 },
                    new Prescription { IdPrescription = 3, Date = new System.DateTime(2020, 1, 5), DueDate = new System.DateTime(2020, 2, 4), IdPatient = 3, IdDoctor = 3 }
                );
            });

            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);
                m.HasMany(e => e.Prescriptions_Medicaments).WithOne(e => e.Medicament).HasForeignKey(e => e.IdMedicament);

                m.HasData(
                    new Medicament { IdMedicament = 1, Name = "Lek na zapalenie", Description = "Lek na zapalenie desc", Type = "Typ leku" },
                    new Medicament { IdMedicament = 2, Name = "Lek na katar", Description = "Lek na katar desc", Type = "Inny typ leku" },
                    new Medicament { IdMedicament = 3, Name = "Lek na kaszel", Description = "Lek na kaszel desc", Type = "Typ leku" }
                );
            });

            modelBuilder.Entity<Prescription_Medicament>(pm =>
            {
                pm.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                pm.Property(e => e.Dose);
                pm.Property(e => e.Details).IsRequired().HasMaxLength(100);

                pm.HasOne(e => e.Medicament).WithMany(e => e.Prescriptions_Medicaments).HasForeignKey(e => e.IdMedicament);
                pm.HasOne(e => e.Prescription).WithMany(e => e.Prescriptions_Medicaments).HasForeignKey(e => e.IdPrescription);

                pm.HasData(
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Details = "Ważne detale nie ma co", Dose = 7 },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 1, Details = "Ważne detale nie ma co", Dose = 8 },
                    new Prescription_Medicament { IdMedicament = 3, IdPrescription = 1, Details = "Ważne detale nie ma co", Dose = 7 },
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 2, Details = "Ważne detale nie ma co", Dose = 4 },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Details = "Ważne detale nie ma co", Dose = 5 },
                    new Prescription_Medicament { IdMedicament = 3, IdPrescription = 2, Details = "Ważne detale nie ma co" },
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 3, Details = "Ważne detale nie ma co" },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 3, Details = "Ważne detale nie ma co" }
                );
            });
        }
    }
}
