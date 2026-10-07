using GestionCitasApi.Data;
using GestionCitasApi.DTOs;
using GestionCitasApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCitasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PacientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> GetPacientes()
        {
            var pacientes = await _context.Pacientes
                .Select(p => new PacienteDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono
                })
                .ToListAsync();

            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDto>> GetPaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            var pacienteDto = new PacienteDto
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Telefono = paciente.Telefono
            };

            return Ok(pacienteDto);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteDto>> PostPaciente(PacienteDto pacienteDto)
        {
            var paciente = new Paciente
            {
                Nombre = pacienteDto.Nombre,
                Telefono = pacienteDto.Telefono
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            pacienteDto.Id = paciente.Id;

            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, pacienteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, PacienteDto pacienteDto)
        {
            if (id != pacienteDto.Id)
            {
                return BadRequest("El ID del parámetro no coincide con el ID del objeto.");
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            paciente.Nombre = pacienteDto.Nombre;
            paciente.Telefono = pacienteDto.Telefono;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound("Paciente no encontrado.");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}