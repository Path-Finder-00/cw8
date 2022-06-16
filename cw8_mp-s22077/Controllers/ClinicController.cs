using cw8_mp_s22077.Models;
using cw8_mp_s22077.Models.DTOs;
using cw8_mp_s22077.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace cw8_mp_s22077.Controllers
{
    [Route("[controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ClinicController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        [Route("doctor/{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            return Ok(await _dbService.GetDoctor(id));
        }
        [HttpPost]
        [Route("doctor")]
        public async Task<IActionResult> AddDoctor (DoctorDTO Doctor)
        {
            return Ok(await _dbService.AddDoctor(Doctor));
        }
        [HttpPut]
        [Route("doctor/{id}")]
        public async Task<IActionResult> UpdateDoctor (int id, DoctorDTO Doctor)
        {
            var updatedDoctor = await _dbService.UpdateDoctor(id, Doctor);

            if (updatedDoctor is null)
            {
                return NotFound("No such doctor has been found in the database.");
            } else
            {
                return Ok("The doctor's data has been successfully updated");
            }
        }
        [HttpDelete]
        [Route("doctor/{id}")]
        public async Task<IActionResult> DeleteDoctor (int id)
        {
            var deletedDoctor = await _dbService.DeleteDoctor(id);

            if (deletedDoctor is null)
            {
                return NotFound("No such doctor has been found in the database.");
            }
            else
            {
                return Ok("The doctor has been successfully deleted");
            }
        }
        [HttpGet]
        [Route("prescription/{id}")]
        public async Task<IActionResult> GetPrescription (int id)
        {
            var prescription = await _dbService.GetPrescription(id);

            if (prescription is null)
            {
                return NotFound("No such prescription has been found in the database.");
            } else
            {
                return Ok(prescription);
            }
        }
    }
}
