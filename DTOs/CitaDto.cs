namespace GestionCitasApi.DTOs
{
    public class CitaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; } = string.Empty;
    }
}