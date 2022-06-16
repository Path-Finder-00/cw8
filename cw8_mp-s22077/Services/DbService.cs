using cw8_mp_s22077.Models;
using cw8_mp_s22077.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace cw8_mp_s22077.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _dbContext;

        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Doctor> GetDoctor (int id)
        {
            return await _dbContext.Doctors.Where(d => d.IdDoctor == id).FirstOrDefaultAsync();
        }

        public async Task<DoctorDTO> AddDoctor (DoctorDTO Doctor)
        {
            _dbContext.Doctors.Add(new Doctor
            {
                FirstName = Doctor.FirstName,
                LastName = Doctor.LastName,
                Email = Doctor.Email
            });

            await _dbContext.SaveChangesAsync();

            return Doctor;
        }
        public async Task<DoctorDTO> UpdateDoctor(int id, DoctorDTO doctor)
        {
            var doctorToUpdate = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == id);
            if (doctorToUpdate == default)
                return null;

            doctorToUpdate.FirstName = doctor.FirstName ?? doctorToUpdate.FirstName;
            doctorToUpdate.LastName = doctor.LastName ?? doctorToUpdate.LastName;
            doctorToUpdate.Email = doctor.Email ?? doctorToUpdate.Email;

            await _dbContext.SaveChangesAsync();

            return doctor;
        }
        public async Task<Doctor> DeleteDoctor(int id)
        {
            var doctorToDelete = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == id);
            if (doctorToDelete == default)
                return null;
            _dbContext.Attach(doctorToDelete);
            _dbContext.Remove(doctorToDelete);

            await _dbContext.SaveChangesAsync();

            return doctorToDelete;
        }
        public async Task<GetPrescriptionDTO> GetPrescription(int id)
        {
            return await _dbContext.Prescriptions
                .Where(p => p.IdPrescription == id)
                .Select(p => new GetPrescriptionDTO
                {
                    IdPrescription = id,
                    Patient = new GetPatientDTO
                    {
                        IdPatient = p.IdPatient,
                        FirstName = p.Patient.FirstName,
                        LastName = p.Patient.LastName,
                        BirthDate = p.Patient.BirthDate
                    },
                    Doctor = new GetDoctorDTO
                    {
                        IdDoctor = p.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email
                    },
                    Medicaments = p.Prescriptions_Medicaments
                        .Select(pm => new GetMedicamentDTO
                        {
                            IdMedicament = pm.IdMedicament,
                            Name = pm.Medicament.Name,
                            Description = pm.Medicament.Description,
                            Type = pm.Medicament.Type
                        })
                        .ToList()
                }).FirstOrDefaultAsync();
        }
    }
}
