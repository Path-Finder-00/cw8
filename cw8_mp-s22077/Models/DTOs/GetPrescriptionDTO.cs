using System.Collections.Generic;

namespace cw8_mp_s22077.Models.DTOs
{
    public class GetPrescriptionDTO
    {
        public int IdPrescription { get; set; }
        public GetPatientDTO Patient { get; set; }
        public GetDoctorDTO Doctor { get; set; }
        public ICollection<GetMedicamentDTO> Medicaments { get; set; }
    }
}
