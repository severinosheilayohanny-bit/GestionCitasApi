using GestionCitasApi.Data;
using GestionCitasApi.DTOs;
using GestionCitasApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCitasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitas()
        {
            var citas = await _context.Citas
                .Include(c => c.Paciente)
                .Select(c => new CitaDto
                {
                    Id = c.Id,
                    Fecha = c.Fecha,
                    Motivo = c.Motivo,
                    PacienteId = c.PacienteId,
                    NombrePaciente = c.Paciente != null ? c.Paciente.Nombre : string.Empty
                })
                .ToListAsync();

            return Ok(citas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDto>> GetCita(int id)
        {
            var cita = await _context.Citas
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null)
            {
                return NotFound("Cita no encontrada.");
            }

            var citaDto = new CitaDto
            {
                Id = cita.Id,
                Fecha = cita.Fecha,
                Motivo = cita.Motivo,
                PacienteId = cita.PacienteId,
                NombrePaciente = cita.Paciente != null ? cita.Paciente.Nombre : string.Empty
            };

            return Ok(citaDto);
        }

        [HttpPost]
        public async Task<ActionResult<CitaDto>> PostCita(CrearCitaDto crearCitaDto)
        {
            // Aqui valido que el paciente exista
            var paciente = await _context.Pacientes.FindAsync(crearCitaDto.PacienteId);
            if (paciente == null)
            {
                return BadRequest("El PacienteId proporcionado no existe.");
            }

            var cita = new Cita
            {
                Fecha = crearCitaDto.Fecha,
                Motivo = crearCitaDto.Motivo,
                PacienteId = crearCitaDto.PacienteId
            };

            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            var citaDto = new CitaDto
            {
                Id = cita.Id,
                Fecha = cita.Fecha,
                Motivo = cita.Motivo,
                PacienteId = cita.PacienteId,
                NombrePaciente = paciente.Nombre
            };

            return CreatedAtAction(nameof(GetCita), new { id = cita.Id }, citaDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, CrearCitaDto crearCitaDto)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound("Cita no encontrada.");
            }

            var paciente = await _context.Pacientes.FindAsync(crearCitaDto.PacienteId);
            if (paciente == null)
            {
                return BadRequest("El PacienteId proporcionado no existe.");
            }

            cita.Fecha = crearCitaDto.Fecha;
            cita.Motivo = crearCitaDto.Motivo;
            cita.PacienteId = crearCitaDto.PacienteId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound("Cita no encontrada.");
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}