using System;

namespace cw8_mp_s22077.Models.DTOs
{
    public class GetPatientDTO
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
