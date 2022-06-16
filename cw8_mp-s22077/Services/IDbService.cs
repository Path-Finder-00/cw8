using cw8_mp_s22077.Models;
using cw8_mp_s22077.Models.DTOs;
using System.Threading.Tasks;

namespace cw8_mp_s22077.Services
{
    public interface IDbService
    {
        public Task<Doctor> GetDoctor(int id);
        public Task<DoctorDTO> AddDoctor(DoctorDTO doctor);
        public Task<DoctorDTO> UpdateDoctor(int id, DoctorDTO doctor);
        public Task<Doctor> DeleteDoctor(int id);
        public Task<GetPrescriptionDTO> GetPrescription(int id);
    }
}
